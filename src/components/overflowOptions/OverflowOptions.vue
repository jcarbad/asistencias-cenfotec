<template>
  <div style="background-color: transparent; width: 100px; margin: auto;">
    <button @click="changeOverflowValue" class="overflow-button">
      <svg
        xmlns="http://www.w3.org/2000/svg"
        width="16"
        height="16"
        fill="currentColor"
        class="bi bi-list"
        viewBox="0 0 16 16"
      >
        <path
          fill-rule="evenodd"
          d="M2.5 12a.5.5 0 0 1 .5-.5h10a.5.5 0 0 1 0 1H3a.5.5 0 0 1-.5-.5m0-4a.5.5 0 0 1 .5-.5h10a.5.5 0 0 1 0 1H3a.5.5 0 0 1-.5-.5m0-4a.5.5 0 0 1 .5-.5h10a.5.5 0 0 1 0 1H3a.5.5 0 0 1-.5-.5"
        />
      </svg>
    </button>
    <div v-show="showOverflowOptions" class="menu-items-container">
      <ul class="item-container-options">
        <MenuItem :itemValue="'Editar'" />
        <MenuItem :itemValue="'Eliminar'" />
      </ul>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref } from "vue";
import MenuItem from "../menuButton/elements/MenuItem.vue";

const showOverflowOptions = ref(false);

const changeOverflowValue = () => {
  showOverflowOptions.value = !showOverflowOptions.value;
};
</script>

<style lang="scss" scoped>
@import "./OverflowOptions.scss";
</style>
