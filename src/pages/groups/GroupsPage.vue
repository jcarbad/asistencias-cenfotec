<template>
  <MainLayout>
    <template #content>
      <h1>Listado de Grupos</h1>
      <DataTable
        :tableHeaders="tableHeaders"
        :rowQuantity="tableData.length"
        additionalInformation
      >
        <template #additionalInformation>
          <MultiUseButton
            :button-type="'primary'"
            :textValue="'Crear Grupo'"
            tabindex="6"
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
                :aria-label="
                  'Editar grupo: ' +
                  item.nivel +
                  '-' +
                  item.grupo +
                  ' del año ' +
                  item.anno
                "
                @click="createGroupEvent(item.id.toString())"
                tabindex="7"
              />
              <MultiUseButton
                :textValue="'Eliminar'"
                :idValue="item.id.toString()"
                :buttonType="'link'"
                :aria-label="
                  'Eliminar grupo: ' +
                  item.nivel +
                  '-' +
                  item.grupo +
                  ' del año ' +
                  item.anno
                "
                @click="deleteGroupById(item.id.toString())"
                tabindex="7"
              />
            </td>
          </tr>
        </template>
      </DataTable>
      <ConfirmModal
        :modalActive="showConfirmationModal"
        :modalType="'confirmation'"
        aria-label="Desea eliminar el registro?"
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
import DataTable from "@/components/dataTable/DataTable.vue";
import MultiUseButton from "@/components/multiUseButton/MultiUseButton.vue";
import { IDataTableInfo } from "@/models/IDataTableInfo";
import ConfirmModal from "@/components/confirmModal/ConfirmModal.vue";
import { useRouter } from "vue-router";
import axios from "axios";
import { onMounted, ref } from "vue";
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
let showInformationModal = ref(false);
let modalMessage = ref("");
let groupIdSelected = "";

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

const noOptionSelectedInModal = () => {
  showConfirmationModal.value = false;
};

const yesOptionSelectedInModal = () => {
  let result = deleteGroupInfoById().valueOf();
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

const deleteGroupById = (groupId: string) => {
  groupIdSelected = groupId;
  showConfirmationModal.value = true;
  document.getElementById(groupId)?.blur()
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

async function deleteGroupInfoById(): Promise<boolean> {
  let response = await axios
    .delete(
      "http://asistencias-api.us-east-1.elasticbeanstalk.com/api/Grupo/Delete/" +
        groupIdSelected,
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
