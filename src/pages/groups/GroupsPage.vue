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
            <td>{{ item.name }}</td>
            <td>{{ item.username }}</td>
            <td>{{ item.email }}</td>
            <td>
              <MultiUseButton :textValue="'Editar'" :buttonType="'link'" />
              <MultiUseButton :textValue="'Eliminar'" :buttonType="'link'" />
            </td>
          </tr>
        </template>
      </DataTable>
    </template>
  </MainLayout>
</template>

<script lang="ts" setup>
import MainLayout from "@/layouts/MainLayout.vue";
import DataTable from "@/components/dataTable/DataTable.vue";
import MultiUseButton from "@/components/multiUseButton/MultiUseButton.vue";
import { IDataTableInfo } from "@/models/IDataTableInfo";
import { useRouter } from "vue-router";
import axios from "axios";
import { onMounted, Ref, ref } from "vue";

interface GroupData {
  id: number;
  name: string;
  username: string;
  email: string;
}

const router = useRouter();

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

const getGroupData = async () => {
  await axios
    .get("https://jsonplaceholder.typicode.com/users")
    .then((response) => {
      for (let row of response.data) {
        tableData.value?.push({
          id: row.id,
          name: row.name,
          email: row.email,
          username: row.username,
        });
      }
    })
    .catch((error) => {
      console.log("Error");
      console.error(error);
    });
};
</script>

<style lang="scss" scoped>
@import "./GroupsPage.scss";
</style>
