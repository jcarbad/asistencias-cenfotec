<template>
  <MainLayout>
    <template #content>
      <header>
        <h1>M&oacute;dulo de Asistencia</h1>
      </header>
      <main>
        <section id="seleccion-grupo">
          <div id="seleccion-grupo-ext" class="container">
            <div id="seleccion-grupo-int" class="row">
              <div id="escoger-grupo" class="col-4">
                <div class="row">
                  <DropdownOptions :title="'Asignatura'" :itemValues="todasAsignaturas"/>
                </div>
                <div class="row">
                  <DropdownOptions :title="'Nivel'" :itemValues="todosNiveles"/>
                </div>
                <div class="row">
                  <DropdownOptions :title="'Grupo'" :itemValues="todosGrupos"/>
                </div>
                <div id="boton-listar" class="row pt-5">
                  <MultiUseButton :textValue="'Listar'" :buttonType="'primary'" />
                </div>
              </div>
              <div id="escoger-fecha" class="col-4">
                  <div id="fecha-picker" class="row col-12">
                    <VueDatePicker
                        v-model="date"
                        locale="es"
                        inline
                        :disabled-week-days="[6, 0]"
                        :enable-time-picker="false"
                    ></VueDatePicker>
                  </div>
              </div>
            </div>
          </div>
        </section>
        <section id="asistencia-grupo">
          <div id="listado-asistencia" class="container d-flex justify-content-center align-items-center full-height">
            <DataTable :tableHeaders="tableHeaders" additionalInformation>
              <template #dataRows>
                <tr v-for="(item, index) in tableData" :key="index">
                  <td>{{ item.id }}</td>
                  <td>{{ item.nombre }}</td>
                  <td>{{ item.apellido1 }}</td>
                  <td>{{ item.apellido2 }}</td>
                  <td>
                    <div id="icono-asistencia" class="d-flex justify-content-center align-items-center full-height">
                      <img v-if="item.asistencia === 'presente'" src="@/assets/checkmark2.png" style="width: 32px; height: 32px;" alt="icono de estudiante presente" />
                      <img v-if="item.asistencia === 'justificada'" src="@/assets/justificada.png" style="width: 32px; height: 32px;" alt="icono de ausencia justificada" />
                      <img v-if="item.asistencia === 'injustificada'" src="@/assets/question-mark.png" style="width: 32px; height: 32px;" alt="icono de ausencia injustificada" />
                      <img v-if="item.asistencia === ''" src="@/assets/empty.png" style="width: 32px; height: 32px;" alt="icono de asistencia de estudiante" />
                    </div>
                  </td>
                  <td>
                      <div id="radio-seleccion-asistencia">
                        <div>
                          <label>
                            <input type="radio" v-model="item.asistencia" value="presente" />
                            Presente
                          </label>
                        </div>
                        <div>
                          <label>
                            <input type="radio" v-model="item.asistencia" value="justificada" />
                            Ausencia Justificada
                          </label>
                        </div>
                        <div>
                          <label>
                            <input type="radio" v-model="item.asistencia" value="injustificada" />
                            Ausencia Injustificada
                          </label>
                        </div>
                      </div>
                  </td>
                </tr>
              </template>
            </DataTable>
          </div>
        </section>
      </main>
    </template>
  </MainLayout>
</template>

<script lang="ts" setup>
import VueDatePicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css'
import MainLayout from "@/layouts/MainLayout.vue";
import DropdownOptions from "@/components/dropdownOptions/DropdownOptions.vue";
import {IDataTableInfo} from "@/models/IDataTableInfo";
import {ref} from "vue";
import MultiUseButton from "@/components/multiUseButton/MultiUseButton.vue";
import DataTable from "@/components/dataTable/DataTable.vue";

const todasAsignaturas = ref([
  {id: "1", value: "Español"},
  {id: "2", value: "Matemática"},
  {id: "3", value: "Inglés"},
  {id: "4", value: "Educación Física"},
  {id: "5", value: "Estudios Sociales"}
]);

const todosNiveles: IDataTableInfo[] = [
  {id: "1", value: "Sétimo"},
  {id: "2", value: "Octavo"},
  {id: "3", value: "Noveno"},
  {id: "4", value: "Décimo"},
  {id: "5", value: "Undécimo"}
];

const todosGrupos: IDataTableInfo[] = [
  {id: "1", value: "Uno"},
  {id: "2", value: "Dos"},
  {id: "3", value: "Tres"},
  {id: "4", value: "Cuatro"},
  {id: "5", value: "Cinco"}
];

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
    value: "PrimerApellido",
  },
  {
    id: "apellido_2",
    value: "Segundo Apellido",
  },
  {
    id: "estado",
    value: "Estado",
  },
  {
    id: "asistencia",
    value: "Asistencia",
  }
];

const tableData = ref([
  {
    id: "111111111",
    nombre: "Armando",
    apellido1: "Ayala",
    apellido2: "Córdoba",
    asistencia: ''
  },
  {
    id: "222222222",
    nombre: "Leonardo",
    apellido1: "Araya",
    apellido2: "Parajeles",
    asistencia: ''
  },
  {
    id: "333333333",
    nombre: "Joan",
    apellido1: "Carballo",
    apellido2: "Badilla",
    asistencia: ''
  },
]);

const date = ref();

</script>

<style lang="scss" scoped>
@import "./AttendancePage.scss";
</style>
