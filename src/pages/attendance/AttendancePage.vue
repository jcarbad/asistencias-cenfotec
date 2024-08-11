<template>
  <MainLayout>
    <template #content>
      <header>
        <h1>Módulo de Asistencia</h1>
      </header>
      <main>
        <section id="seleccion-grupo">
          <div id="seleccion-grupo-ext" class="container">
            <div id="seleccion-grupo-int" class="row">
              <div id="escoger-grupo" class="col-4">
                <div class="row">
                  <DropdownOptions
                    v-model="asignaturaSeleccioada"
                    :title="'Asignatura'"
                    :itemValues="todasAsignaturas"
                  />
                </div>
                <!-- <div class="row">
                  <DropdownOptions :title="'Nivel'" :itemValues="todosNiveles"/>
                </div>
                <div class="row">
                  <DropdownOptions :title="'Grupo'" :itemValues="todosGrupos"/>
                </div> -->
                <div id="boton-listar" class="row pt-5">
                  <MultiUseButton
                    :textValue="'Listar'"
                    :buttonType="'primary'"
                    @click="loadStudentsList"
                  />
                </div>
              </div>
              <div id="escoger-fecha" class="col-4">
                <div id="fecha-picker" class="row col-12">
                  <VueDatePicker
                    v-model="date"
                    locale="es"
                    inline
                    tabindex="-1"
                    :disabled-week-days="[6, 0]"
                    :enable-time-picker="false"
                    readonly
                  ></VueDatePicker>
                </div>
              </div>
            </div>
          </div>
        </section>
        <section id="asistencia-grupo">
          <div
            id="listado-asistencia"
            class="container d-flex justify-content-center align-items-center full-height"
          >
            <DataTable
              v-if="tableData.length > 0"
              :tableHeaders="tableHeaders"
              additionalInformation
            >
              <template #dataRows>
                <tr v-for="(item, index) in tableData" :key="index">
                  <td>{{ item.id }}</td>
                  <td>{{ item.nombre }}</td>
                  <td>{{ item.apellido1 }}</td>
                  <td>{{ item.apellido2 }}</td>
                  <td>
                    <div
                      id="icono-asistencia"
                      class="d-flex justify-content-center align-items-center full-height"
                    >
                      <!-- <img v-if="item.asistencia === 'presente'" src="@/assets/checkmark2.png" style="width: 32px; height: 32px;" alt="icono de estudiante presente" /> -->
                      <img
                        v-if="item.asistencia === 'ST1'"
                        src="@/assets/justificada.png"
                        style="width: 32px; height: 32px"
                        aria-label="icono de ausencia justificada"
                      />
                      <img
                        v-if="item.asistencia === 'ST2'"
                        src="@/assets/question-mark.png"
                        style="width: 32px; height: 32px"
                        aria-label="icono de ausencia injustificada"
                      />
                      <img
                        v-if="item.asistencia === ''"
                        src="@/assets/empty.png"
                        style="width: 32px; height: 32px"
                        aria-label="icono de asistencia de estudiante"
                      />
                    </div>
                  </td>
                  <td>
                    <div id="radio-seleccion-asistencia">
                      <div>
                        <label>
                          <input
                            :id="'T-' + item.estudianteId"
                            type="radio"
                            v-model="item.asistencia"
                            value="ST1"
                            :aria-label="
                              'Asignar tardía a: ' +
                              item.nombre +
                              ' ' +
                              item.apellido1
                            "
                            tabindex="7"
                          />
                          Tardía
                        </label>
                      </div>
                      <div>
                        <label>
                          <input
                            :id="'A-' + item.estudianteId"
                            type="radio"
                            v-model="item.asistencia"
                            value="ST2"
                            :aria-label="
                              'Asignar ausencia a: ' +
                              item.nombre +
                              ' ' +
                              item.apellido1
                            "
                            tabindex="7"
                          />
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
        <MultiUseButton
          v-if="tableData.length > 0"
          :textValue="'Registrar'"
          aria-label="Guardar Ausencias"
          @click="saveStudentInfo"
        />
      </main>
      <ConfirmModal
        :modalActive="showInformationModal"
        :modalType="'information'"
        @closeModal="closeInformationModal"
      >
        <template #content>
          <h2>{{ modalMessage }}</h2>
        </template>
      </ConfirmModal>
    </template>
  </MainLayout>
</template>

<script lang="ts" setup>
import VueDatePicker from "@vuepic/vue-datepicker";
import "@vuepic/vue-datepicker/dist/main.css";
import MainLayout from "@/layouts/MainLayout.vue";
import DropdownOptions from "@/components/dropdownOptions/DropdownOptions.vue";
import { IDataTableInfo } from "@/models/IDataTableInfo";
import { ref } from "vue";
import MultiUseButton from "@/components/multiUseButton/MultiUseButton.vue";
import DataTable from "@/components/dataTable/DataTable.vue";
import axios from "axios";
import { useUserStore } from "@/store/useUserStore";
import ConfirmModal from "@/components/confirmModal/ConfirmModal.vue";

interface StudentInformation {
  estudianteId: string;
  id: string;
  nombre: string;
  apellido1: string;
  apellido2: string;
  asistencia: string;
}

const userStore = useUserStore();

let showInformationModal = ref(false);
let modalMessage = ref("");

const todasAsignaturas = ref([{ id: "Asg1", value: "Estudios Sociales" }]);

const todosNiveles: IDataTableInfo[] = [{ id: "3", value: "Noveno" }];

const todosGrupos: IDataTableInfo[] = [
  { id: "1", value: "Uno" },
  { id: "2", value: "Dos" },
  { id: "3", value: "Tres" },
  { id: "4", value: "Cuatro" },
  { id: "5", value: "Cinco" },
];

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
  },
];

const tableData = ref<StudentInformation[]>([]);

const date = ref();
const asignaturaSeleccioada = ref("");

const closeInformationModal = () => {
  showInformationModal.value = false;
  window.location.reload();
};

const loadStudentsList = () => {
  if (asignaturaSeleccioada.value !== "") {
    getStudentListInfo();
  }
};

const saveStudentInfo = () => {
  if (registrarAusencias().valueOf()) {
    modalMessage.value = "Registros guardados correctamente";
  } else {
    modalMessage.value = "Ha ocurrido un error. Intente más tarde";
  }
  showInformationModal.value = true;
};

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
          asistencia: "",
        });
      }
    })
    .catch((error) => {
      console.dir(error);
    });
}

async function registrarAusencias(): Promise<boolean> {
  let movimientos = [];

  for (let rowAux of tableData.value) {
    if (rowAux.asistencia !== "") {
      movimientos.push({
        estudianteId: rowAux.estudianteId,
        estadoId: rowAux.asistencia,
        cantidad: 1,
      });
    }
  }

  const data = JSON.stringify({
    id: "0",
    asignatura: asignaturaSeleccioada.value,
    grupo: "ce12a1b8-fafb-4ebf-908d-5cbc0e113684",
    creadoPor: "929ddcf9-f777-4462-9650-1a67439f0cd3",
    registros: movimientos,
  });

  let response = await axios
    .post(
      "http://asistencias-api.us-east-1.elasticbeanstalk.com/api/RegistroAusencias/Create",
      data,
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
@import "./AttendancePage.scss";
</style>
