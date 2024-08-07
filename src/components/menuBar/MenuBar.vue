<template>
    <div class="ayala-menu-bar">
      <div>
        <img
          src="@/assets/logo principal.png"
          alt="Logo principal de la compañía"
          class="company-icon"
        />
        <MenuButton :username="userStore.currentUser" :nvdaTextValue="'Gestión de Usuario'"/>
      </div>
    </div>
</template>

<script lang="ts" setup>
import { useUserStore } from "@/store/useUserStore";
import MenuButton from "@/components/menuButton/MenuButton.vue";

const userStore = useUserStore();

</script>

<style lang="scss" scoped>
@import "./MenuBar.scss";
</style>
