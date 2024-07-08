<template>
    <div class="ayala-menu-bar">
      <div>
        <img
          src="@/assets/logo principal.png"
          alt="ayala"
          class="company-icon"
        />
        <MenuButton />
      </div>
    </div>
</template>

<script lang="ts" setup>
import MenuButton from "@/components/menuButton/MenuButton.vue";
</script>

<style lang="scss" scoped>
@import "./MenuBar.scss";
</style>
