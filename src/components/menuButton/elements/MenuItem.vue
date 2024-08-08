<template>
    <li class="menu-item" @click="customEmitEvent" tabindex="1" :id="props.itemValue">
        {{ props.itemValue }}
    </li>
</template>

<script lang="ts" setup>
// eslint-disable-next-line
const props = defineProps({
    itemValue:{
        type: String,
        required: true,
    }
});

// eslint-disable-next-line
const emit = defineEmits(["logout"])

const customEmitEvent = () => {
    emit("logout")
}

</script>

<style lang="scss" scoped>
@import './MenuItem.scss'
</style>