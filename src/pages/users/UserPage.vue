<template>
  <MainLayout>
    <template #content>
      <h1>Información Administrativo</h1>
      <FormContainer style="width: 50vw; margin-left: auto; margin-right: auto">
        <template #formInformation>
          <fieldset>
            <div class="container">
              <div class="row">
                <div class="col-sm table-col-content col-right">
                  <TextField
                    v-model="adminAux.id"
                    :textFieldType="'text'"
                    :textFieldHeader="'Identificación'"
                  />
                </div>
                <div class="col-sm table-col-content col-left">
                  <TextField
                    v-model="adminAux.nombre"
                    :textFieldType="'text'"
                    :textFieldHeader="'Nombre'"
                  />
                </div>
              </div>
              <div class="row">
                <div class="col-sm table-col-content col-right">
                  <TextField
                    v-model="adminAux.apellido1"
                    :textFieldType="'text'"
                    :textFieldHeader="'Primer Apellido'"
                  />
                </div>
                <div class="col-sm table-col-content col-left">
                  <TextField
                    v-model="adminAux.apellido2"
                    :textFieldType="'text'"
                    :textFieldHeader="'Segundo Apellido'"
                  />
                </div>
              </div>
              <div class="row">
                <div class="col-sm table-col-content col-right">
                  <TextField
                    v-model="adminAux.email"
                    :textFieldType="'text'"
                    :textFieldHeader="'Email'"
                  />
                </div>
                <div class="col-sm table-col-content col-left">
                  <DropdownOptions
                    v-model="adminAux.estatus"
                    :title="'Estado'"
                    :itemValues="estadoItems"
                  />
                </div>
              </div>
              <div class="row">
                <div class="col-sm">
                  <MultiUseButton
                    :textValue="'Guardar'"
                    aria-label="Guardar Información de Administrativo"
                    @click="saveAdminInfo"
                  />
                  <MultiUseButton
                    :textValue="'Regresar'"
                    :buttonType="'secondary'"
                    aria-label="Regresar al Listado de Administrativos"
                    @click="goBackToAdminList"
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
import DropdownOptions from "@/components/dropdownOptions/DropdownOptions.vue";
import MultiUseButton from "@/components/multiUseButton/MultiUseButton.vue";
import TextField from "@/components/textField/TextField.vue";
import ConfirmModal from "@/components/confirmModal/ConfirmModal.vue";
import { IDataTableInfo } from "@/models/IDataTableInfo";
import { useRoute, useRouter } from "vue-router";
import { onMounted, ref } from "vue";
import { useUserStore } from "@/store/useUserStore";
import axios from "axios";

interface AdminInfo {
  administrativoId: string;
  id: string;
  nombre: string;
  apellido1: string;
  apellido2: string;
  email: string;
  estatus: string;
}

const router = useRouter();
const route = useRoute();
const userStore = useUserStore();

let showInformationModal = ref(false);
let modalMessage = ref("");

let adminAux = ref<AdminInfo>({
  administrativoId: route.params.id[0],
  id: "",
  nombre: "",
  apellido1: "",
  apellido2: "",
  email: "",
  estatus: "",
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
    getAdminById(route.params.id.toString());
  }
});

const closeInformationModal = () => {
  goBack();
};

const goBackToAdminList = async () => {
  goBack();
};

const saveAdminInfo = () => {
  const data = JSON.stringify({
    administrativoId: route.params.id,
    id: adminAux.value.id,
    nombre: adminAux.value.nombre,
    apellido1: adminAux.value.apellido1,
    apellido2: adminAux.value.apellido2,
    email: adminAux.value.email,
    estatus: adminAux.value.estatus,
  });

  let result = null;
  if (adminAux.value.administrativoId === "0") {
    result = create(data);
  } else {
    result = update(data);
  }

  if (result.valueOf() && adminAux.value.administrativoId === "0") {
    modalMessage.value = "El registro se ha guardado correctamente";
  } else if (result.valueOf() && adminAux.value.administrativoId !== "0") {
    modalMessage.value = "El registro se ha actualizado correctamente";
  } else {
    modalMessage.value = "El registro no se ha guardado correctamente";
  }
  showInformationModal.value = true;
};

async function create(data: string): Promise<boolean> {
  let result = await axios
    .post(
      "http://asistencias-api.us-east-1.elasticbeanstalk.com/api/Administrativo/Create",
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
      "http://asistencias-api.us-east-1.elasticbeanstalk.com/api/Administrativo/Update",
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

async function getAdminById(groupId: string) {
  await axios
    .get(
      "http://asistencias-api.us-east-1.elasticbeanstalk.com/api/Administrativo/Get/" +
        groupId,
      {
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + userStore.getAccessToken,
        },
      }
    )
    .then((response) => {
      for (let row of response.data.result) {
        adminAux.value.administrativoId = row.administrativoId;
        adminAux.value.id = row.id;
        adminAux.value.nombre = row.nombre;
        adminAux.value.apellido1 = row.apellido1;
        adminAux.value.apellido2 = row.apellido2;
        adminAux.value.email = row.email;
        adminAux.value.estatus = row.estatus;
      }
    })
    .catch((error) => {
      console.dir(error);
    });
}

function goBack() {
  router.push({
    name: "users",
  });
}
</script>

<style lang="scss" scoped>
@import "./UserPage.scss";
</style>
