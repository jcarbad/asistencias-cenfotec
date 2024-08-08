<template>
  <a :class="{'left-bar-item': !showOnlyIcon, 'left-bar-item-collapsed': showOnlyIcon}" @click="routerLinkEvent">
    <svg
      xmlns="http://www.w3.org/2000/svg"
      width="20"
      height="20"
      fill="currentColor"
      class="bi bi-house-door-fill"
      :class="{'icon-collapsed' : showOnlyIcon, 'icon-non-collapsed' : !showOnlyIcon}"
      viewBox="0 0 16 16"
      :aria-label="iconValue"
    >
      <path
        d="M6.5 14.5v-3.505c0-.245.25-.495.5-.495h2c.25 0 .5.25.5.5v3.5a.5.5 0 0 0 .5.5h4a.5.5 0 0 0 .5-.5v-7a.5.5 0 0 0-.146-.354L13 5.793V2.5a.5.5 0 0 0-.5-.5h-1a.5.5 0 0 0-.5.5v1.293L8.354 1.146a.5.5 0 0 0-.708 0l-6 6A.5.5 0 0 0 1.5 7.5v7a.5.5 0 0 0 .5.5h4a.5.5 0 0 0 .5-.5"
      /></svg>
      <span v-if="!props.showOnlyIcon">{{ props.name }}</span>
  </a>
</template>

<script lang="ts" setup>
import { ref } from "vue";
import { useRouter } from "vue-router";

// eslint-disable-next-line
const props = defineProps({
  name: {
    type: String,
    required: true,
  },
  link: {
    type: String,
    required: true,
  },
  showOnlyIcon: {
    type: Boolean,
    required: true,
    default: false
  }
});

const router = useRouter();

const routerLinkEvent = () => {
  router.push({
    name: props.link,
  });
};

let iconValue = props.showOnlyIcon ? ref("Ir al módulo de " + props.name) : ref("Ir al módulo de " + props.name)
</script>

<style lang="scss" scoped>
@import "./LeftBarItem.scss";
</style>
