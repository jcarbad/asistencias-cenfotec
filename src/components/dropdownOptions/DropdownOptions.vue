<template>
  <div style="margin-top: 10px; width: 300px;">
    <label class="dropdown-label-standard">
      {{ props.title }}
      <select class="dropdown-standard" @change="changeValueEvent">
        <option id="" value="">Seleccione</option>
        <option v-for="item in props.itemValues" :id="item.id" :key="item.id" :value="item.id">
          {{ item.value }}
        </option>
      </select>
    </label>
  </div>
</template>

<script lang="ts" setup>
import { defineEmits } from 'vue';
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
  modelValue: {
    type: String,
    default: ""
  },
});

const emit = defineEmits(['update:modelValue'])

const changeValueEvent = ($event: any) =>{
  console.dir($event.target.value)
  emit('update:modelValue', $event.target.value)
}
</script>

<style lang="scss" scoped>
@import "./DropdownOptions.scss";
</style>
