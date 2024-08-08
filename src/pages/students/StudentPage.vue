<template>
  <MainLayout>
    <template #content>
      <h1>Información Estudiante</h1>
      <FormContainer style="width: 50vw; margin-left: auto; margin-right: auto">
        <template #formInformation>
          <fieldset>
            <div class="container">
              <div class="row">
                <div class="col-sm table-col-content col-right">
                  <TextField
                    v-model="studentAux.id"
                    :textFieldType="'text'"
                    :textFieldHeader="'Identificación'"
                  />
                </div>
                <div class="col-sm table-col-content col-left">
                  <TextField
                    v-model="studentAux.nombre"
                    :textFieldType="'text'"
                    :textFieldHeader="'Nombre'"
                  />
                </div>
              </div>
              <div class="row">
                <div class="col-sm table-col-content col-right">
                  <TextField
                    v-model="studentAux.apellido1"
                    :textFieldType="'text'"
                    :textFieldHeader="'Primer Apellido'"
                  />
                </div>
                <div class="col-sm table-col-content col-left">
                  <TextField
                    v-model="studentAux.apellido2"
                    :textFieldType="'text'"
                    :textFieldHeader="'Segundo Apellido'"
                  />
                </div>
              </div>
              <div class="row">
                <div class="col-sm table-col-content col-right">
                  <TextField
                    v-model="studentAux.responsableId"
                    :textFieldType="'text'"
                    :textFieldHeader="'Id. Responsable'"
                  />
                </div>
                <div class="col-sm table-col-content col-left">
                  <TextField
                    v-model="studentAux.responsableNombreCompleto"
                    :textFieldType="'text'"
                    :textFieldHeader="'Nombre Completo Responsable'"
                  />
                </div>
              </div>
              <div class="row">
                <div class="col-sm table-col-content col-right">
                  <TextField
                    v-model="studentAux.responsableTelefono"
                    :textFieldType="'text'"
                    :textFieldHeader="'Teléfono Responsable'"
                  />
                </div>
                <div class="col-sm table-col-content col-left">
                  <TextField
                    v-model="studentAux.responsableEmail"
                    :textFieldType="'text'"
                    :textFieldHeader="'Email'"
                  />
                </div>
              </div>
              <div class="row">
                <div class="col-sm table-col-content col-right"></div>
                <div class="col-sm table-col-content col-left">
                  <DropdownOptions
                    v-model="studentAux.estatus"
                    :title="'Estado'"
                    :itemValues="estadoItems"
                  />
                </div>
              </div>
              <div class="row">
                <div class="col-sm">
                  <MultiUseButton
                    :textValue="'Guardar'"
                    aria-label="Guardar Información de Estudiante"
                    @click="saveStudentInfo"
                  />
                  <MultiUseButton
                    :textValue="'Regresar'"
                    :buttonType="'secondary'"
                    aria-label="Regresar al Listado de Estudiantes"
                    @click="goBackToStudentList"
                  />
                </div>
              </div>
            </div>
          </fieldset>
        </template>
      </FormContainer>
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
import MainLayout from "@/layouts/MainLayout.vue";
import FormContainer from "@/components/formContainer/FormContainer.vue";
import TextField from "@/components/textField/TextField.vue";
import MultiUseButton from "@/components/multiUseButton/MultiUseButton.vue";
import DropdownOptions from "@/components/dropdownOptions/DropdownOptions.vue";
import ConfirmModal from "@/components/confirmModal/ConfirmModal.vue";
import { IDataTableInfo } from "@/models/IDataTableInfo";
import { useRoute, useRouter } from "vue-router";
import { useUserStore } from "@/store/useUserStore";
import { onMounted, ref } from "vue";
import axios from "axios";

interface StudentInfo {
  estudianteId: string;
  id: string;
  nombre: string;
  apellido1: string;
  apellido2: string;
  responsableId: string;
  responsableNombreCompleto: string;
  responsableTelefono: string;
  responsableEmail: string;
  estatus: string;
}

const router = useRouter();
const route = useRoute();
const userStore = useUserStore();

let showInformationModal = ref(false);
let modalMessage = ref("");

let studentAux = ref<StudentInfo>({
  estudianteId: route.params.id[0],
  id: "",
  nombre: "",
  apellido1: "",
  apellido2: "",
  responsableId: "",
  responsableNombreCompleto: "",
  responsableTelefono: "",
  responsableEmail: "",
  estatus: ""
});

const estadoItems: IDataTableInfo[] = [
  {
    id: "Activo",
    value: "Activo",
  },
  {
    id: "Inactivo",
    value: "Inactivo",
  },
];

onMounted(() => {
  if (route.params.id !== "0") {
    getStudentById(route.params.id.toString());
  }
});

const closeInformationModal = () => {
  goBack();
};

const goBackToStudentList = async () => {
  goBack();
};

const saveStudentInfo = () => {
  const data = JSON.stringify({
    estudianteId: route.params.id,
    id: studentAux.value.id,
    nombre: studentAux.value.nombre,
    apellido1: studentAux.value.apellido1,
    apellido2: studentAux.value.apellido2,
    responsableId: studentAux.value.responsableId,
    responsableNombreCompleto: studentAux.value.responsableNombreCompleto,
    responsableTelefono: studentAux.value.responsableTelefono,
    responsableEmail: studentAux.value.responsableEmail,
    creadoPor: "929ddcf9-f777-4462-9650-1a67439f0cd3",
    estatus: studentAux.value.estatus,
  });

  let result = null;
  if (studentAux.value.estudianteId === "0") {
    result = create(data);
  } else {
    result = update(data);
  }

  if (result.valueOf() && studentAux.value.estudianteId === "0") {
    modalMessage.value = "El registro se ha guardado correctamente";
  } else if (result.valueOf() && studentAux.value.estudianteId !== "0") {
    modalMessage.value = "El registro se ha actualizado correctamente";
  } else {
    modalMessage.value = "El registro no se ha guardado correctamente";
  }
  showInformationModal.value = true;
};

async function create(data: string): Promise<boolean> {
  let result = await axios
    .post(
      "http://asistencias-api.us-east-1.elasticbeanstalk.com/api/Estudiante/Create",
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
  return result;
}

async function update(data: string) {
  let result = await axios
    .patch(
      "http://asistencias-api.us-east-1.elasticbeanstalk.com/api/Estudiante/Update",
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
  return result;
}

async function getStudentById(estudianteId: string) {
  await axios
    .get(
      "http://asistencias-api.us-east-1.elasticbeanstalk.com/api/Estudiante/Get/" +
        estudianteId,
      {
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + userStore.getAccessToken,
        },
      }
    )
    .then((response) => {
      for (let row of response.data.result) {
        studentAux.value.estudianteId = row.estudianteId;
        studentAux.value.id = row.id;
        studentAux.value.nombre = row.nombre;
        studentAux.value.apellido1 = row.apellido1;
        studentAux.value.apellido2 = row.apellido2;
        studentAux.value.responsableId = row.responsableId;
        studentAux.value.responsableNombreCompleto = row.responsableNombreCompleto;
        studentAux.value.responsableTelefono = row.responsableTelefono;
        studentAux.value.responsableEmail = row.responsableEmail;
        studentAux.value.estatus = row.estatus;
      }
    })
    .catch((error) => {
      console.dir(error);
    });
}

function goBack() {
  router.push({
    name: "students",
  });
}
</script>

<style lang="scss" scoped>
@import "./StudentPage.scss";
</style>
