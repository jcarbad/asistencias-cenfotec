<template>
  <div style="margin-top: 10px; width: 300px;">
    <label class="dropdown-label-standard">
      {{ props.title }}
      <select class="dropdown-standard">
        <option id="">Seleccione</option>
        <option v-for="item in props.itemValues" :id="item.id" :key="item.id">
          {{ item.value }}
        </option>
      </select>
    </label>
  </div>
</template>

<script lang="ts" setup>
import { IDataTableInfo } from "@/models/IDataTableInfo";
import { PropType } from "vue";

// eslint-disable-next-line
const props = defineProps({
  title: {
    type: String,
    required: true,
    default: "Opciones",
  },
  itemValues: {
    type: Array as PropType<Array<IDataTableInfo>>,
    required: true,
  },
});
</script>

<style lang="scss" scoped>
@import "./DropdownOptions.scss";
</style>
