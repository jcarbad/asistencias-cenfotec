<template>
  <div :class="{'left-bar' :!isLeftBarCollapsed, 'left-bar-collapsed' : isLeftBarCollapsed}">
      <svg
        xmlns="http://www.w3.org/2000/svg"
        width="20"
        height="20"
        fill="currentColor"
        class="bi bi-plus-circle"
        viewBox="0 0 16 16"
        style="color: white; float: right; margin-top: 10px; margin-right: 20px;"
        @click="collapseLeftBar"
      >
        <path
          d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14m0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16"
        />
        <path
          d="M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4"
        />
      </svg>
    <ul>
      <LeftBarItem :name="'Grupos'" :link="'groups'" :showOnlyIcon="isLeftBarCollapsed" />
      <LeftBarItem :name="'Administrativos'" :link="'users'" :showOnlyIcon="isLeftBarCollapsed" />
      <LeftBarItem :name="'Estudiantes'" :link="'students'" :showOnlyIcon="isLeftBarCollapsed" />
      <LeftBarItem :name="'Asistencia'" :link="'attendance'" :showOnlyIcon="isLeftBarCollapsed" />
    </ul>
  </div>
</template>

<script lang="ts" setup>
import { ref } from "vue";
import LeftBarItem from "./elements/LeftBarItem.vue";

const isLeftBarCollapsed = ref(false);

const collapseLeftBar = () => {
    isLeftBarCollapsed.value = !isLeftBarCollapsed.value
    /*if(isLeftBarCollapsed.value){
      document.getElementById("main-container")!.style.paddingLeft = "60px";
    } else {
      document.getElementById("main-container")!.style.paddingLeft = "200px";
    }*/
}
</script>

<style lang="scss" scoped>
@import "./LeftBar.scss";
</style>
