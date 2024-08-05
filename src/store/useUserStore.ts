import { defineStore } from "pinia";

export const useUserStore = defineStore("useUserStore", {
  state: () => ({
    currentUser: "",
    accessToken: "",
  }),
  getters: {
    getCurrentUser: (state) => {
      return state.currentUser;
    },
    getAccessToken: (state) => {
      return state.accessToken;
    },
  },
  actions: {
    setCurrentUser(value: string) {
      if (value) {
        this.currentUser = value;
      }
    },
    setAccessToken(value: string) {
      if (value) {
        this.accessToken = value;
      }
    },
    resetStateValues(){
        this.currentUser = ""
        this.accessToken = ""
    }
  },
  persist: true
});
