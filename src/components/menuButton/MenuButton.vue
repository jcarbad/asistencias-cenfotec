<template>
  <button
      class="menu-button"
      alt="Abrir menú de acciones de usuario"
      :class="{'menu-button-selected' : isMenuOptionsVisible}"
      @click="openMenuOptions">
    {{ props.username }}
    <svg
      xmlns="http://www.w3.org/2000/svg"
      width="16"
      height="16"
      fill="currentColor"
      class="bi bi-caret-down-square"
      viewBox="0 0 16 16"
    >
      <path
        d="M3.626 6.832A.5.5 0 0 1 4 6h8a.5.5 0 0 1 .374.832l-4 4.5a.5.5 0 0 1-.748 0z"
      />
      <path
        d="M0 2a2 2 0 0 1 2-2h12a2 2 0 0 1 2 2v12a2 2 0 0 1-2 2H2a2 2 0 0 1-2-2zm15 0a1 1 0 0 0-1-1H2a1 1 0 0 0-1 1v12a1 1 0 0 0 1 1h12a1 1 0 0 0 1-1z"
      />
    </svg>
  </button>
  <div v-show="isMenuOptionsVisible" class="menu-items-container">
    <ul class="item-container-options">
      <MenuItem :itemValue="'Configuración'" />
      <MenuItem :itemValue="'Cerrar sesión'" @logout="logoutEvent" />
    </ul>
  </div>
</template>

<script lang="ts" setup>
import { ref } from 'vue';
import MenuItem from './elements/MenuItem.vue'
import { useRouter } from 'vue-router';
import { useUserStore } from "@/store/useUserStore";

// eslint-disable-next-line
const props = defineProps({
  username: {
    type: String,
    required: true,
    default: "username",
  },
});

const router = useRouter();
const userStore = useUserStore();

const isMenuOptionsVisible = ref(false)

const openMenuOptions = () => {
  isMenuOptionsVisible.value = !isMenuOptionsVisible.value
}

const logoutEvent = () => {
  userStore.resetStateValues()
  router.push({name: 'login'})
}

</script>

<style lang="scss" scoped>
@import './MenuButton.scss';
</style>
