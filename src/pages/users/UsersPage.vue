<template>
  <MainLayout>
    <template #content>
      <h1>Listado Administrativos</h1>
      <DataTable
        :tableHeaders="tableHeaders"
        :rowQuantity="tableData.length"
        additionalInformation
      >
        <template #additionalInformation>
          <MultiUseButton
            :button-type="'primary'"
            :textValue="'Crear Administrativo'"
            tabindex="6"
            @click="createAdminEvent('0')"
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
                  'Editar administrativo: ' + item.nombre + ' ' + item.apellido1
                "
                tabindex="7"
                @click="createAdminEvent(item.id.toString())"
              />
              <MultiUseButton
                :textValue="'Eliminar'"
                :buttonType="'link'"
                :aria-label="
                  'Eliminar administrativo: ' + item.nombre + ' ' + item.apellido1
                "
                tabindex="7"
                @click="deleteAdminById(item.id.toString())"
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
import axios from "axios";
import { onMounted, ref } from "vue";
import { useUserStore } from "@/store/useUserStore";
import { IDataTableInfo } from "@/models/IDataTableInfo";
import { useRouter } from "vue-router";

interface AdminData {
  id: string;
  nombre: string;
  apellido1: string;
  apellido2: string;
  email: string;
}

const router = useRouter();
const userStore = useUserStore();
let showConfirmationModal = ref(false);
let showInformationModal = ref(false);
let modalMessage = ref("");
let adminIdSelected = "";

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

const tableData = ref<AdminData[]>([]);

const createAdminEvent = (id: string) => {
  router.push({
    name: "user",
    params: {
      id: id,
    },
  });
};

const deleteAdminById = (groupId: string) => {
  adminIdSelected = groupId;
  showConfirmationModal.value = true;
};

const getAdminData = () => {
  getAdminListInfo();
};

const noOptionSelectedInModal = () => {
  showConfirmationModal.value = false;
};

const yesOptionSelectedInModal = () => {
  let result = deleteAdminInfoById().valueOf();
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

onMounted(() => {
  getAdminData();
});

async function getAdminListInfo() {
  await axios
    .get(
      "http://asistencias-api.us-east-1.elasticbeanstalk.com/api/Administrativo/Get",
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
          id: row.administrativoId,
          nombre: row.nombre,
          apellido1: row.apellido1,
          apellido2: row.apellido2,
          email: row.email,
        });
      }
    })
    .catch((error) => {
      console.dir(error);
    });
}

async function deleteAdminInfoById(): Promise<boolean> {
  let response = await axios
    .delete(
      "http://asistencias-api.us-east-1.elasticbeanstalk.com/api/Administrativo/Delete/" +
        adminIdSelected,
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
@import "./UsersPage.scss";
</style>
