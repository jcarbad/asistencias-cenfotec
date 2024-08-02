import { defineStore } from "pinia";

export const useUserStore = defineStore("useUserStore", {
  state: () => ({
    currentUser: "",
  }),
  getters: {
    getCurrentUser: (state) => {
      return state.currentUser;
    },
  },
  actions: {
    setCurrentUser(value: string) {
      if (value) {
        this.currentUser = value;
      }
    },
    resetStateValues(){
        this.currentUser = ""
    }
  },
  persist: true
});
