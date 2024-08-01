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
                  <TextField :textFieldType="'text'" :textFieldHeader="'Id. Responsable'"/>
                </div>
                <div class="col-sm table-col-content col-left">
                  <TextField :textFieldType="'text'" :textFieldHeader="'Nombre Completo Responsable'"/>
                </div>
              </div>
              <div class="row">
                <div class="col-sm table-col-content col-right">
                  <TextField :textFieldType="'text'" :textFieldHeader="'Teléfono Responsable'"/>
                </div>
                <div class="col-sm table-col-content col-left">
                  <TextField :textFieldType="'text'" :textFieldHeader="'Email'"/>
                </div>
              </div>
              <div class="row">
                <div class="col-sm table-col-content col-right"></div>
                <div class="col-sm table-col-content col-left">
                  <DropdownOptions
                    :title="'Estado'"
                    :itemValues="estadoItems"
                  />
                </div>
              </div>
              <div class="row">
                <div class="col-sm">
                  <MultiUseButton :textValue="'Guardar'" @click="saveStudentInfo"/>
                  <MultiUseButton :textValue="'Regresar'" :buttonType="'secondary'" @click="saveStudentInfo"/>
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
import TextField from "@/components/textField/TextField.vue";
import MultiUseButton from "@/components/multiUseButton/MultiUseButton.vue";
import DropdownOptions from "@/components/dropdownOptions/DropdownOptions.vue";
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

const saveStudentInfo = () => {
  router.push({
    name: 'students',
  });
};
</script>

<style lang="scss" scoped>
@import "./StudentPage.scss";
</style>
