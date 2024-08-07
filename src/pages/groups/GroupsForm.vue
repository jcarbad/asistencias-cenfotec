<template>
  <MainLayout>
    <template #content>
      <h1>Información de Grupo</h1>
      <FormContainer style="width: 45vw; margin-left: auto; margin-right: auto">
        <template #formInformation>
          <fieldset>
            <div class="container">
              <div class="row">
                <div class="col-sm table-col-content col-right">
                  <DropdownOptions
                    v-model="grupoAux.nivel"
                    :title="'Nivel'"
                    :itemValues="nivelItems"
                  />
                </div>
                <div class="col-sm table-col-content col-left">
                  <DropdownOptions
                    v-model="grupoAux.grupoNumero"
                    :title="'Grupo'"
                    :itemValues="grupoItems"
                  />
                </div>
              </div>
              <div class="row">
                <div class="col-sm table-col-content col-right">
                  <DropdownOptions
                    v-model="grupoAux.anno"
                    :title="'Año'"
                    :itemValues="annoItems"
                  />
                </div>
                <div class="col-sm table-col-content col-left">
                  <DropdownOptions
                    v-model="grupoAux.estatus"
                    :title="'Estado'"
                    :itemValues="estadoItems"
                  />
                </div>
              </div>
              <div class="row">
                <div class="col-sm">
                  <MultiUseButton
                    :textValue="'Guardar'"
                    @click="saveGroupInfo"
                  />
                  <MultiUseButton
                    :textValue="'Regresar'"
                    :buttonType="'secondary'"
                    @click="goBackToGroupList"
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
import ConfirmModal from "@/components/confirmModal/ConfirmModal.vue";
import { IDataTableInfo } from "@/models/IDataTableInfo";
import { useRoute, useRouter } from "vue-router";
import axios from "axios";
import { useUserStore } from "@/store/useUserStore";
import { onMounted, ref } from "vue";

interface GroupInfo {
  grupoId: string;
  nivel: string;
  grupoNumero: string;
  subGrupo: string;
  anno: string;
  guia: string;
  institutoId: string;
  estatus: string;
}

const router = useRouter();
const route = useRoute();
const userStore = useUserStore();

let grupoAux = ref<GroupInfo>({
  grupoId: route.params.id[0],
  nivel: "0",
  grupoNumero: "0",
  subGrupo: "A",
  anno: "0",
  guia: "929ddcf9-f777-4462-9650-1a67439f0cd3",
  institutoId: "Ins1",
  estatus: "",
});

let showInformationModal = ref(false);
let modalMessage = ref("");

const nivelItems: IDataTableInfo[] = [
  {
    id: "7",
    value: "Séptimo",
  },
  {
    id: "8",
    value: "Octavo",
  },
  {
    id: "9",
    value: "Noveno",
  },
  {
    id: "10",
    value: "Décimo",
  },
  {
    id: "11",
    value: "Undécimo",
  },
];

const grupoItems: IDataTableInfo[] = [
  {
    id: "1",
    value: "I",
  },
  {
    id: "2",
    value: "II",
  },
  {
    id: "3",
    value: "III",
  },
  {
    id: "4",
    value: "IV",
  },
  {
    id: "5",
    value: "V",
  },
];

const annoItems: IDataTableInfo[] = [
  {
    id: "2024",
    value: "2024",
  },
];

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
  console.log(route.params.id);
  if (route.params.id !== "0") {
    getGroupById(route.params.id.toString());
  }
});

const goBackToGroupList = () => {
  goBack();
};

const saveGroupInfo = () => {
  const data = JSON.stringify({
    grupoId: grupoAux.value.grupoId,
    nivel: grupoAux.value.nivel,
    grupoNumero: grupoAux.value.grupoNumero,
    subGrupo: "A",
    anno: grupoAux.value.anno,
    guia: "929ddcf9-f777-4462-9650-1a67439f0cd3",
    institutoId: "Ins1",
    estatus: grupoAux.value.estatus,
  });

  let result = null;
  if (grupoAux.value.grupoId === "0") {
    result = create(data);
  } else {
    result = update(data);
  }

  console.log(result.valueOf());
  if (result.valueOf() && grupoAux.value.grupoId === "0") {
    modalMessage.value = "El registro se ha guardado correctamente";
  } else if (result.valueOf() && grupoAux.value.grupoId !== "0") {
    modalMessage.value = "El registro se ha actualizado correctamente";
  } else {
    modalMessage.value = "El registro no se ha guardado correctamente";
  }
  showInformationModal.value = true;
};

const closeInformationModal = () => {
  goBack();
};

async function create(data: string): Promise<boolean> {
  let response = await axios
    .post(
      "http://asistencias-api.us-east-1.elasticbeanstalk.com/api/Grupo/Create",
      data,
      {
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + userStore.getAccessToken,
        },
      }
    )
    .then((result) => {
      return true;
    })
    .catch((error) => {
      return false;
    });

  return response;
}

async function update(data: string) {
  await axios
    .patch(
      "http://asistencias-api.us-east-1.elasticbeanstalk.com/api/Grupo/Update",
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
}

async function getGroupById(groupId: string) {
  await axios
    .get(
      "http://asistencias-api.us-east-1.elasticbeanstalk.com/api/Grupo/Get/" +
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
        grupoAux.value.grupoId = row.grupoId;
        grupoAux.value.nivel = row.nivel.toString();
        grupoAux.value.grupoNumero = row.grupoNumero.toString();
        grupoAux.value.estatus = row.estatus.toString().replace(/ /g, "");
        grupoAux.value.anno = row.anno.toString();
      }
    })
    .catch((error) => {
      console.dir(error);
    });
}

function goBack() {
  router.push({
    name: "groups",
  });
}
</script>

<style lang="scss" scoped>
@import "./GroupsForm.scss";
</style>
