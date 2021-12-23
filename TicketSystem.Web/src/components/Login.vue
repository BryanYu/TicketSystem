<template>
<!DOCTYPE html>
<html lang="en">

<head>
    <title>Ticket System Login</title>
</head>

<body class="bg-gradient-primary">

    <div class="container">

        <!-- Outer Row -->
        <div class="row justify-content-center">
            <div class="col-xl-12 col-lg-12">
                <div class="card o-hidden border-0 shadow-lg my-5">
                    <div class="card-body p-0">
                        <!-- Nested Row within Card Body -->
                        <div class="row justify-content-center">
                            <div class="col-lg-6">
                                <div class="p-5">
                                    <div class="text-center">
                                        <h1 class="h4 text-gray-900 mb-4">Welcome Back!</h1>
                                    </div>
                                    <div class="user">
                                        <div class="form-group">
                                            <input type="text" v-model="account" class="form-control form-control-user"
                                                id="account" placeholder="Enter Your Account">
                                        </div>
                                        <div class="form-group">
                                            <input type="password" v-model="password" class="form-control form-control-user"
                                                id="password" placeholder="Password">
                                        </div>
                                        <button @click="login" type="button" class="btn btn-primary btn-user btn-block">Login</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>

        </div>

    </div>
</body>

</html>


</template>


<script>
import constant from '../common/constant'
import dataService from '../services/dataService'
export default {
    data() {
            return {
                account: '',
                password:''
            }
        },
    methods: {
        login() {
             dataService.login(this.account, this.password)
             .then(result => {
                 if(result.data.code === 0) {
                    sessionStorage.setItem(constant.token,result.data.data);
                    alert('Login Success');
                    this.$router.push('/');
                }
             }).catch(dataService.handleError);
        }
    }
}
</script>