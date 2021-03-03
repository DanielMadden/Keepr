<template lang="">
  <div id="my-nav-bar" class="container-fluid" @click="clearVaultDelete()">
    <div id="my-nav-row" class="row">
      <div class="col-2 my-nav-column d-flex justify-content-start align-items-center">
        <button
          id="nav-logo-container"
          class="d-flex justify-content-center align-items-center"
          @click="homePage()"
        >
          <div
            class="nav-logo-spinner d-flex justify-content-center align-items-center"
          >
            <i class="fas fa-certificate"></i>
          </div>
          <div
            class="nav-logo-logo no-stretch d-flex justify-content-center align-items-center"
          >
            <i class="fab fa-kickstarter-k"></i>
          </div>
        </button>
      </div>
      <form
        @submit.prevent="search()"
        id="my-nav-search-column"
        class="col-8 my-nav-column d-flex justify-content-center align-items-center px-0"
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
      <div id="my-nav-search-column" class="col-2 my-nav-column d-flex justify-content-end align-items-center">
        <button
          class="my-nav-button"
          @click="login"
          v-if="!user.isAuthenticated"
        >
          Login
        </button>
        <button
          id="nav-button-profile"
          class="my-nav-button d-flex align-items-center"
          :style="`background: 0, 0, url('${user.picture}') no-repeat center center /cover; overflow-y: hidden`"
          @click="openProfile()"
          v-else
        >
          <span>
            <!-- class="mr-3" -->
            <!-- <i class="far fa-user"></i> -->
          </span>
          <!-- <span>
            {{ user.name }}
          </span> -->
        </button>
      </div>
    </div>
  </div>
</template>
<script>
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'
import { AuthService } from '../services/AuthService'
import { useRouter } from 'vue-router'
export default {
  setup() {
    // Variables
    const router = useRouter()
    const form = reactive({ text: '' })
    const user = computed(() => AppState.user)
    const account = computed(() => AppState.account)
    // Functions
    const homePage = () => { router.push({ name: 'Home' }) }
    const search = () => {
    }
    const login = async () => { AuthService.loginWithPopup() }
    const openProfile = () => {
      router.push({ path: '/profile/' + account.value.id })
    }
    // Clear deletes for vaults (so they can click ANYWHERE)
    const clearVaultDelete = () => { AppState.deleting.vault = false }
    return {
      // Variables
      form,
      user,
      // Functions
      homePage,
      search,
      login,
      openProfile,
      clearVaultDelete
    }
  }
}
</script>
<style scoped>
@import "../assets/css/navbar.css";
</style>
