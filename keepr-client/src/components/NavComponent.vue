<template lang="">
  <div id="my-nav-bar" class="container-fluid">
    <div id="my-nav-row" class="row">
      <div class="col-3 my-nav-column d-flex justify-content-start align-items-center">
        <button
          id="nav-home-button"
          class="d-flex justify-content-center align-items-center"
        >
          <h1 class="m-0">
            K
          </h1>
        </button>
      </div>
      <form
        @submit.prevent="search()"
        id="my-nav-search-column"
        class="col-6 my-nav-column d-flex justify-content-center align-items-center"
      >
        <!-- <form @submit.prevent="search()"> -->
        <div id="nav-search-box">
          <input
            id="nav-search-input"
            class="my-nav-bar-item"
            type="text"
            placeholder="Search..."
            v-model="form.text"
          />
          <div id="nav-search-icon" class="d-flex justify-content-center align-items-center">
            <i class="fas fa-search"></i>
          </div>
        </div>
        <!-- </form> -->
      </form>
      <div id="my-nav-search-column" class="col-3 my-nav-column d-flex justify-content-end align-items-center">
        <button
          class="my-nav-button"
          @click="login"
          v-if="!user.isAuthenticated"
        >
          Login
        </button>
        <button
          class="my-nav-button d-flex align-items-center"
          v-else
        >
          <span class="mr-3">
            <i class="far fa-user"></i>
          </span>
          <span>
            Daniel Madden
          </span>
        </button>
      </div>
    </div>
  </div>
</template>
<script>
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'
import { AuthService } from '../services/AuthService'
export default {
  setup() {
    // Variables
    const form = reactive({ text: '' })
    const user = computed(() => AppState.user)
    // Functions
    const login = async () => { AuthService.loginWithPopup() }
    const search = () => {
    }
    return {
      // Variables
      form,
      user,
      // Functions
      login,
      search
    }
  }
}
</script>
<style scoped>
@import "../assets/css/navbar.css";
</style>
