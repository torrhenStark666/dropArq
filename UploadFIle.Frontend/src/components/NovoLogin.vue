<template>
  <div class="centered-container">
    <form novalidate class="md-layout" @submit.prevent="validateUser">
      <md-card class="md-layout-item md-size-200 md-small-size-100 ">
        <md-card-header>
          <div class="md-title">Nova Conta</div>
          Entre com os dados a seguir para criar uma nova conta:
        </md-card-header>

        <md-card-content>
          <div class="md-layout">
              <md-field :class="getValidationClass('name')">
                <label for="name">Nome Completo</label>
                <md-input name="name" id="name" v-model="form.name" :disabled="sending" />
                <span class="md-error" v-if="!$v.form.name.required">Campo Obrigatório</span>
                <span class="md-error" v-if="!$v.form.name.minLength">Tamanho minimo para nome: 3 digitos!</span>
              </md-field>
            
              <md-field :class="getValidationClass('email')">
                <label for="email">Email</label>
                <md-input type="email" name="email" id="email" autocomplete="email" v-model="form.email" :disabled="sending" />
                <span class="md-error" v-if="!$v.form.email.required">Campo Obrigatório</span>
                <span class="md-error" v-else-if="!$v.form.email.email">Email inválido</span>
              </md-field>
              <md-field :class="getValidationClass('password')">
                <label for="password">Senha</label>
                <md-input type="password" name="password" id="password" v-model="form.password" :disabled="sending" />
                <span class="md-error" v-if="!$v.form.password.required">Campo Obrigatório</span>
                <span class="md-error" v-if="!$v.form.password.minLength">Tamanho minimo para senha: 6 digitos!</span>
              </md-field>
            
          </div>
        </md-card-content>

        <md-progress-bar md-mode="indeterminate" v-if="sending" />

        <md-card-actions>
          <md-button type="submit" class="md-primary" :disabled="sending">Pronto!</md-button>
        </md-card-actions>
      </md-card>
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
    name: 'FormValidation',
    mixins: [validationMixin],
    data: () => ({
      form: {
        password: null,
        name: null,
        email: null,
      },
      sending: false
    }),
    validations: {
      form: {
        name: {
          required,
          minLength: minLength(3)
        },
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
      getValidationClass (fieldName) {
        const field = this.$v.form[fieldName]

        if (field) {
          return {
            'md-invalid': field.$invalid && field.$dirty
          }
        }
      },
      clearForm () {
        this.$v.$reset()
        this.form.name = null
        this.form.password = null
        this.form.email = null
      },
      saveUser () {
        this.sending = true
        this
          .$axios
            .post(
              'http://localhost:5000/api/User', 
              this.form, 
              { 
              }
           )
          .then( response => {
            console.log(response);
            this.$router.push('/');
            this.clearForm();
          })
          .catch( err =>{
            console.log(err);
            alert('Não foi possivel criar um novo usuário, entre em contato com o ADM do sistema.');
          })
          .finally( () =>{
            this.sending = false;
          })
      },
      validateUser () {
        this.$v.$touch()

        if (!this.$v.$invalid) {
          this.saveUser()
        }
      }
    }
  }
</script>

<style lang="scss" scoped>
  .md-progress-bar {
    position: absolute;
    top: 0;
    right: 0;
    left: 0;
  }
  .centered-container {
  display: flex;
  align-items: center;
  justify-content: center;
  position: relative;
  height: 75vh;
  }
</style>
