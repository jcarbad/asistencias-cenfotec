<template>
  <MainLayout>
    <template #content>
      <h1>Listado de Grupos</h1>
      <DataTable :tableHeaders="tableHeaders" additionalInformation>
        <template #additionalInformation>
            <MultiUseButton :button-type="'primary'" :textValue="'Crear Grupo'" @click="createGroupEvent('0')" />
        </template>
        <template #dataRows>
          <tr v-for="(item, index) in tableData" :key="index">
            <td>{{ item.id }}</td>
            <td>{{ item.nivel }}</td>
            <td>{{ item.grupo }}</td>
            <td>{{ item.anno }}</td>
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

const router = useRouter();

const tableHeaders : IDataTableInfo[] = [
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

const tableData = [
  {
    id: "1",
    nivel: "Octavo",
    grupo: "I",
    anno: "2024",
    acciones: "",
  },
  {
    id: "2",
    nivel: "Octavo",
    grupo: "II",
    anno: "2024",
    acciones: "",
  },
];

const createGroupEvent = (id:string) => {
  router.push({
    name: 'group',
    params: {
      id: id
    }
  });
};

</script>

<style lang="scss" scoped>
@import "./GroupsPage.scss";
</style>
