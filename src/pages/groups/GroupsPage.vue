<template>
  <MainLayout>
    <template #content>
      <h1>Listado de Grupos</h1>
      <DataTable :tableHeaders="tableHeaders" additionalInformation>
        <template #additionalInformation>
          <MultiUseButton
            :button-type="'primary'"
            :textValue="'Crear Grupo'"
            @click="createGroupEvent('0')"
          />
        </template>
        <template #dataRows>
          <tr v-for="(item, index) in tableData" :key="index">
            <td>{{ item.id }}</td>
            <td>{{ item.nivel }}</td>
            <td>{{ item.grupo }}</td>
            <td>{{ item.anno }}</td>
            <td>
              <MultiUseButton
                :textValue="'Editar'"
                :buttonType="'link'"
                @click="createGroupEvent(item.id.toString())"
              />
              <MultiUseButton
                :textValue="'Eliminar'"
                :buttonType="'link'"
                @click="deleteGroupById(item.id.toString())"
              />
            </td>
          </tr>
        </template>
      </DataTable>
      <ConfirmModal
        :modalActive="showConfirmationModal"
        @closeModal="closeConfirmationModal"
      >
        <template #content>
          <label>El registro ha sido eliminado correctamente</label>
        </template>
      </ConfirmModal>
    </template>
  </MainLayout>
</template>

<script lang="ts" setup>
import MainLayout from "@/layouts/MainLayout.vue";
import DataTable from "@/components/dataTable/DataTable.vue";
import MultiUseButton from "@/components/multiUseButton/MultiUseButton.vue";
import { IDataTableInfo } from "@/models/IDataTableInfo";
import ConfirmModal from "@/components/confirmModal/ConfirmModal.vue";
import { useRouter } from "vue-router";
import axios from "axios";
import { onMounted, Ref, ref } from "vue";
import { useUserStore } from "@/store/useUserStore";

interface GroupData {
  id: number;
  nivel: string;
  grupo: string;
  anno: string;
}

const router = useRouter();
const userStore = useUserStore();
let showConfirmationModal = ref(false);
let modalMessage = ref("");

const tableHeaders: IDataTableInfo[] = [
  {
    id: "id",
    value: "ID",
  },
  {
    id: "nivel",
    value: "Nivel",
  },
  {
    id: "grupo",
    value: "Grupo",
  },
  {
    id: "anno",
    value: "Año",
  },
  {
    id: "acciones",
    value: "Acciones",
  },
];

let tableData = ref<GroupData[]>([]);

const createGroupEvent = (id: string) => {
  router.push({
    name: "group",
    params: {
      id: id,
    },
  });
};

onMounted(() => {
  getGroupData();
});

const getGroupData = () => {
  getGroupListInfo();
};

const closeConfirmationModal = () => {
  showConfirmationModal.value = false;
  window.location.reload();
};

const deleteGroupById = async (groupId: string) => {
  let result = (await deleteGroupInfoById(groupId)).valueOf();
  if (result) {
    modalMessage.value = "El registro ha sido eliminado correctamente";
    showConfirmationModal.value = true;
  }
};

async function getGroupListInfo() {
  await axios
    .get(
      "http://asistencias-api.us-east-1.elasticbeanstalk.com/api/Grupo/Get",
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
          id: row.grupoId,
          nivel: row.nivel,
          grupo: row.grupoNumero,
          anno: row.anno,
        });
      }
    })
    .catch((error) => {
      console.dir(error);
    });
}

async function deleteGroupInfoById(groupId: string): Promise<boolean> {
  let response = await axios
    .delete(
      "http://asistencias-api.us-east-1.elasticbeanstalk.com/api/Grupo/Delete/" +
        groupId,
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
@import "./GroupsPage.scss";
</style>
