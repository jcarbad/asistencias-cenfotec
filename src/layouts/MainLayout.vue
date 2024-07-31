<template>
    <header><MenuBar/></header>
    <nav><LeftBar/></nav>
    <main class="main-container" id="main-container">
        <div class="internal-container">
            <slot name="content"></slot>
        </div>
    </main>
</template>

<script lang="ts" setup>
import MenuBar from '@/components/menuBar/MenuBar.vue'
import LeftBar from '@/components/leftBar/LeftBar.vue'
</script>

<style lang="scss" scoped>
@import "./MainLayout.scss";
</style>