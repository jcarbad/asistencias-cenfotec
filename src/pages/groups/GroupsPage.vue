<template>
    <MainLayout>
        <template #content>
            <div>
                <h1>Groups Page</h1>
                <DataTable :tableHeaders="tableHeaders">
                    <template #dataRows>
                        <tr v-for="item, index in tableData" :key="index">
                            <td>{{ item.id }}</td>
                            <td>{{ item.nivel }}</td>
                            <td>{{ item.grupo }}</td>
                            <td>{{ item.anno }}</td>
                            <td><button>Modificar</button></td>
                        </tr>
                    </template>
                </DataTable>
            </div>
        </template>
    </MainLayout>
</template>

<script lang="ts" setup>
import MainLayout from '@/layouts/MainLayout.vue';
import DataTable from '@/components/dataTable/DataTable.vue';

const tableHeaders = [
    {
        id: "id",
        value: "ID"
    },
    {
        id: "nivel",
        value: "Nivel"
    },
    {
        id: "grupo",
        value: "Grupo"
    },
    {
        id: "anno",
        value: "Año"
    },
    {
        id: "acciones",
        value: "Acciones"
    },
]

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

</script>

<style lang="scss" scoped>
@import "./GroupsPage.scss";
</style>