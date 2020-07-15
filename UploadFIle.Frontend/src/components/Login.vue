<template>
  <div class="centered-container">
    <form novalidate class="md-layout" @submit.prevent="validateUser">
    <md-content class="md-elevation-2">

      <div class="title">
        <div class="md-title">UploadFile.Client</div>
        <div class="md-body-1">Acesse o sistema:</div>
      </div>
      
        <div class="form">
          <md-field  :class="getValidationClass('email')">
            <label>E-mail</label>
            <md-input v-model="login.email" autofocus></md-input>
            <span class="md-error" v-if="!$v.login.email.required">Campo Obrigatório</span>
            <span class="md-error" v-if="!$v.login.email.email">Email inválido</span>
          </md-field>

          <md-field md-has-password  :class="getValidationClass('password')">
            <label>Password</label>
            <md-input v-model="login.password" type="password"></md-input>
            <span class="md-error" v-if="!$v.login.password.required">Campo Obrigatório</span>
          </md-field>
        </div>
      
        <div class="actions md-layout md-alignment-center-space-between">
          <router-link to="/NewAccount">Nova Conta</router-link>
          <md-button type="submit" class="md-raised md-primary">Entrar!</md-button>
        </div>
      
      <div class="loading-overlay" v-if="loading">
        <md-progress-spinner md-mode="indeterminate" :md-stroke="2"></md-progress-spinner>
      </div>

    </md-content>
    <div class="background" />
    </form>
  </div>
</template>

<script>
import { validationMixin } from 'vuelidate'
  import {
    required,
    email,
    minLength
  } from 'vuelidate/lib/validators'

export default {
  name: "App",
  mixins: [validationMixin],
  data() {
    return {
      loading: false,
      login: {
        email: "",
        password: ""
      }
    };
  },
  validations: {
      login: {
        password: {
          required,
          minLength: minLength(6)
        },
        email: {
          required,
          email
        }
      }
  },
  methods: {
    auth() {
      // your code to login user
      // this is only for example of loading
      this.loading = true;

      this
          .$axios
            .post(
              'http://localhost:5000/api/User/login', 
              this.login
           )
          .then( response => {
            this.$auth = 'Bearer '  + response.data.accessToken
            localStorage.token = 'Bearer '  + response.data.accessToken
            this.$idUser = response.data.userId
            localStorage.idUser = response.data.userId
            this.$nameUser = response.data.userName
            localStorage.userName = response.data.userName
            this.$router.push('/Dashboard');
            this.clearForm();
          })
          .catch( err =>{
            err;
            alert('Não foi possivel efetuar o login, entre em contato com o ADM do sistema.');
          })
          .finally( () =>{
            this.loading = false;
          })
    },
    validateUser () {
        this.$v.$touch()

        if (!this.$v.$invalid) {
          this.auth()
        }
      },
      getValidationClass (fieldName) {
        const field = this.$v.login[fieldName]

        if (field) {
          return {
            'md-invalid': field.$invalid && field.$dirty
          }
        }
      },
      clearForm () {
        this.$v.$reset()
        this.login.email = null
        this.login.password = null
      }
  }
};
</script>
<style lang="scss">
.centered-container {
  display: flex;
  align-items: center;
  justify-content: center;
  position: relative;
  height: 75vh;
  .title {
    text-align: center;
    margin-bottom: 30px;
  }
  .actions {
    .md-button {
      margin: 0;
    }
  }
  .form {
    margin-bottom: 60px;
  }

  .md-content {
    z-index: 1;
    padding: 40px;
    width: 100%;
    max-width: 400px;
    position: relative;
  }
  .loading-overlay {
    z-index: 10;
    top: 0;
    left: 0;
    right: 0;
    position: absolute;
    width: 100%;
    height: 100%;
    background: rgba(255, 255, 255, 0.9);
    display: flex;
    align-items: center;
    justify-content: center;
  }
}
</style>
