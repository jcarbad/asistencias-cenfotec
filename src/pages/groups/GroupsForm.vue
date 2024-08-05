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
    </template>
  </MainLayout>
</template>

<script lang="ts" setup>
import MainLayout from "@/layouts/MainLayout.vue";
import FormContainer from "@/components/formContainer/FormContainer.vue";
import DropdownOptions from "@/components/dropdownOptions/DropdownOptions.vue";
import MultiUseButton from "@/components/multiUseButton/MultiUseButton.vue";
import { IDataTableInfo } from "@/models/IDataTableInfo";
import { useRoute, useRouter } from "vue-router";
import axios from "axios";
import { useUserStore } from "@/store/useUserStore";
import { ref } from "vue";

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
    id: "SP1",
    value: "Activo",
  },
  {
    id: "SP2",
    value: "Inactivo",
  },
];

const goBackToGroupList = async () => {
  goBack();
};

const saveGroupInfo = async () => {
  const data = JSON.stringify({
    grupoId: route.params.id,
    nivel: grupoAux.value.nivel,
    grupoNumero: grupoAux.value.grupoNumero,
    subGrupo: "A",
    anno: grupoAux.value.anno,
    guia: "929ddcf9-f777-4462-9650-1a67439f0cd3",
    institutoId: "Ins1",
    estatus: grupoAux.value.estatus,
  });

  await axios
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
    .then((response) => {
      router.push({
        name: "groups",
      });
    })
    .catch((error) => {
      console.dir(error);
    });
};

function goBack() {
  router.push({
    name: "groups",
  });
}
</script>

<style lang="scss" scoped>
@import "./GroupsForm.scss";
</style>
