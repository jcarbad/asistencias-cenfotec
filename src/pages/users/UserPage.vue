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
                  <TextField :textFieldType="'text'" :textFieldHeader="'Identificación'"/>
                </div>
                <div class="col-sm table-col-content col-left">
                  <TextField :textFieldType="'text'" :textFieldHeader="'Nombre'"/>
                </div>
              </div>
              <div class="row">
                <div class="col-sm table-col-content col-right">
                  <TextField :textFieldType="'text'" :textFieldHeader="'Primer Apellido'"/>
                </div>
                <div class="col-sm table-col-content col-left">
                  <TextField :textFieldType="'text'" :textFieldHeader="'Segundo Apellido'"/>
                </div>
              </div>
              <div class="row">
                <div class="col-sm table-col-content col-right">
                  <TextField :textFieldType="'text'" :textFieldHeader="'Email'"/>
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
                  <MultiUseButton :textValue="'Guardar'" @click="saveUserInfo"/>
                  <MultiUseButton :textValue="'Regresar'" :buttonType="'secondary'" @click="saveUserInfo"/>
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
import TextField from "@/components/textField/TextField.vue";
import { IDataTableInfo } from "@/models/IDataTableInfo";
import { useRouter } from "vue-router";

const router = useRouter();

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

const saveUserInfo = () => {
  router.push({
    name: 'users',
  });
};
</script>

<style lang="scss" scoped>
@import "./UserPage.scss";
</style>
