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
              <div id="escoger-grupo" class="col-6">
                <DropdownOptions :title="'Asignatura'" :itemValues="todasAsignaturas"/>
                <DropdownOptions :title="'Nivel'" :itemValues="todosNiveles"/>
                <DropdownOptions :title="'Grupo'" :itemValues="todosGrupos"/>
              </div>
              <div id="escoger-fecha" class="col-6 container">
                  <div id="fecha-picker" class="row">
                    (( DATE PICKER ))
                  </div>
                  <div id="boton-listar" class="row">
                    <MultiUseButton :textValue="'Listar'" :buttonType="'primary'" />
                  </div>

              </div>
            </div>
          </div>
        </section>
        <section id="asistencia-grupo"></section>
      </main>
    </template>
  </MainLayout>
</template>

<script lang="ts" setup>
import MainLayout from "@/layouts/MainLayout.vue";
import DropdownOptions from "@/components/dropdownOptions/DropdownOptions.vue";
import {IDataTableInfo} from "@/models/IDataTableInfo";
import {ref} from "vue";
import MultiUseButton from "@/components/multiUseButton/MultiUseButton.vue";

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

</script>

<style lang="scss" scoped>
@import "./AttendancePage.scss";
</style>
