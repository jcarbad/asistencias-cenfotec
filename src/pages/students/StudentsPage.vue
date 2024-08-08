<template>
  <MainLayout>
    <template #content>
      <h1>Listado Estudiantes</h1>
      <DataTable
        :tableHeaders="tableHeaders"
        :rowQuantity="tableData.length"
        additionalInformation
      >
        <template #additionalInformation>
          <MultiUseButton
            :button-type="'primary'"
            :textValue="'Crear Estudiante'"
            tabindex="6"
            @click="createStudentEvent('0')"
          />
        </template>
        <template #dataRows>
          <tr v-for="(item, index) in tableData" :key="index">
            <td>{{ item.id }}</td>
            <td>{{ item.nombre }}</td>
            <td>{{ item.apellido1 }}</td>
            <td>{{ item.apellido2 }}</td>
            <td>
              <MultiUseButton
                :textValue="'Editar'"
                :buttonType="'link'"
                :aria-label="
                  'Editar estudiante: ' + item.nombre + ' ' + item.apellido1
                "
                tabindex="7"
                @click="createStudentEvent(item.estudianteId.toString())"
              />
              <MultiUseButton
                :textValue="'Eliminar'"
                :buttonType="'link'"
                :aria-label="
                  'Eliminar estudiante: ' + item.nombre + ' ' + item.apellido1
                "
                tabindex="7"
                @click="deleteStudentById(item.estudianteId.toString())"
              />
            </td>
          </tr>
        </template>
      </DataTable>
      <ConfirmModal
        :modalActive="showConfirmationModal"
        :modalType="'confirmation'"
        @noOptionSelected="noOptionSelectedInModal"
        @yesOptionSelected="yesOptionSelectedInModal"
      >
        <template #content>
          <h2>Desea eliminar el registro?</h2>
        </template>
      </ConfirmModal>
      <ConfirmModal
        :modalActive="showInformationModal"
        :modalType="'information'"
        @closeModal="closeInformationModal"
      >
        <template #content>
          <h2>El registro ha sido eliminado correctamente</h2>
        </template>
      </ConfirmModal>
    </template>
  </MainLayout>
</template>

<script lang="ts" setup>
import MainLayout from "@/layouts/MainLayout.vue";
import MultiUseButton from "@/components/multiUseButton/MultiUseButton.vue";
import DataTable from "@/components/dataTable/DataTable.vue";
import ConfirmModal from "@/components/confirmModal/ConfirmModal.vue";
import { IDataTableInfo } from "@/models/IDataTableInfo";
import { useRouter } from "vue-router";
import { useUserStore } from "@/store/useUserStore";
import { onMounted, ref } from "vue";
import axios from "axios";

interface StudentData {
  estudianteId: string;
  id: string;
  nombre: string;
  apellido1: string;
  apellido2: string;
}

const router = useRouter();
const userStore = useUserStore();
let showConfirmationModal = ref(false);
let showInformationModal = ref(false);
let modalMessage = ref("");
let studentIdSelected = "";

const tableHeaders: IDataTableInfo[] = [
  {
    id: "id",
    value: "Identificación",
  },
  {
    id: "nombre",
    value: "Nombre",
  },
  {
    id: "apellido_1",
    value: "Apellido 1",
  },
  {
    id: "apellido_2",
    value: "Apellido 2",
  },
  {
    id: "acciones",
    value: "Acciones",
  },
];

const tableData = ref<StudentData[]>([]);

const createStudentEvent = (id: string) => {
  router.push({
    name: "student",
    params: {
      id: id,
    },
  });
};

const deleteStudentById = (studentId: string) => {
  studentIdSelected = studentId;
  showConfirmationModal.value = true;
};

const noOptionSelectedInModal = () => {
  showConfirmationModal.value = false;
};

const yesOptionSelectedInModal = () => {
  let result = deleteStudentInfoById().valueOf();
  showConfirmationModal.value = false;
  if (result) {
    modalMessage.value = "El registro ha sido eliminado correctamente";
  } else {
    modalMessage.value = "El registro no ha sido eliminado correctamente";
  }
  showInformationModal.value = true;
};

const closeInformationModal = () => {
  showInformationModal.value = false;
  window.location.reload();
};

const getStudentData = () => {
  getStudentListInfo();
};

onMounted(() => {
  getStudentData();
});

async function getStudentListInfo() {
  await axios
    .get(
      "http://asistencias-api.us-east-1.elasticbeanstalk.com/api/Estudiante/Get",
      {
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + userStore.getAccessToken,
        },
      }
    )
    .then((response) => {
      for (let row of response.data.result) {
        tableData.value?.push({
          estudianteId: row.estudianteId,
          id: row.id,
          nombre: row.nombre,
          apellido1: row.apellido1,
          apellido2: row.apellido2,
        });
      }
    })
    .catch((error) => {
      console.dir(error);
    });
}

async function deleteStudentInfoById(): Promise<boolean> {
  let response = await axios
    .delete(
      "http://asistencias-api.us-east-1.elasticbeanstalk.com/api/Estudiante/Delete/" +
        studentIdSelected,
      {
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + userStore.getAccessToken,
        },
      }
    )
    .then((response) => {
      return true;
    })
    .catch((error) => {
      return false;
    });

  return response;
}

</script>

<style lang="scss" scoped>
@import "./StudentsPage.scss";
</style>
