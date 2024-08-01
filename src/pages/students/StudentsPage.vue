<template>
  <MainLayout>
    <template #content>
      <h1>Listado Estudiantes</h1>
      <DataTable :tableHeaders="tableHeaders" additionalInformation>
        <template #additionalInformation>
          <MultiUseButton
            :button-type="'primary'"
            :textValue="'Crear Estudiante'"
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
import MultiUseButton from "@/components/multiUseButton/MultiUseButton.vue";
import DataTable from "@/components/dataTable/DataTable.vue";
import { IDataTableInfo } from "@/models/IDataTableInfo";
import { useRouter } from "vue-router";

const router = useRouter();

const tableHeaders : IDataTableInfo[] = [
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

const tableData = [
  {
    id: "111111111",
    nombre: "Armando",
    apellido1: "Ayala",
    apellido2: "Córdoba",
    acciones: "",
  },
  {
    id: "222222222",
    nombre: "Leonardo",
    apellido1: "Araya",
    apellido2: "Parajeles",
    acciones: "",
  },
  {
    id: "333333333",
    nombre: "Joan",
    apellido1: "Carballo",
    apellido2: "Badilla",
    acciones: "",
  },
];

const createStudentEvent = (id:string) => {
  router.push({
    name: 'student',
    params: {
      id: id
    }
  });
};

</script>

<style lang="scss" scoped>
@import "./StudentsPage.scss";
</style>
