<template>
  <MainLayout>
    <template #content>
      <h1>Información de Grupo</h1>
      <FormContainer style="width: 50vw; margin-left: auto; margin-right: auto">
        <template #formInformation>
          <fieldset>
            <div class="container">
              <div class="row">
                <div class="col-sm table-col-content col-left">
                  <DropdownOptions :title="'Nivel'" :itemValues="nivelItems" />
                </div>
                <div class="col-sm table-col-content col-left">
                  <DropdownOptions :title="'Grupo'" :itemValues="grupoItems" />
                </div>
              </div>
              <div class="row">
                <div class="col-sm table-col-content col-left">
                  <DropdownOptions :title="'Año'" :itemValues="annoItems" />
                </div>
                <div class="col-sm table-col-content col-left">
                  <DropdownOptions
                    :title="'Estado'"
                    :itemValues="estadoItems"
                  />
                </div>
              </div>
              <div class="row">
                <div class="col-sm">
                  <MultiUseButton :textValue="'Guardar'" @click="saveGroupInfo"/>
                  <MultiUseButton :textValue="'Regresar'" :buttonType="'secondary'" @click="saveGroupInfo"/>
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
import { useRouter } from "vue-router";

const router = useRouter();

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

const saveGroupInfo = () => {
  router.push({
    name: 'groups',
  });
};
</script>

<style lang="scss" scoped>
@import "./GroupsForm.scss";
</style>
