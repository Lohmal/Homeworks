package com.volkankivanc.tarsusogrencibilgisistemi.Giris

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import com.volkankivanc.tarsusogrencibilgisistemi.R
import kotlinx.android.synthetic.main.activity_giris2.*

class Giris2 : AppCompatActivity() {

    var emailTextact2:String? =null
    var phoneNumber:String? =null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_giris2)
    }

    fun act1(view: View){
        val intent= Intent(applicationContext, Giris1::class.java)
        startActivity(intent)
    }
    fun act3(view: View){
        val intent= Intent(applicationContext, Giris3::class.java)
        startActivity(intent)
    }

    fun sifirla(view: View){
        emailTextact2=editTextTextEmailAddress2act2.text.toString()
        phoneNumber=editTextTextPhone.text.toString()
        if(emailTextact2.isNullOrEmpty()){
            editTextTextEmailAddress2act2.error = "Boş bırakılmaz"
        }
        if(phoneNumber.isNullOrEmpty()){
            editTextTextPhone.error = "Boş bırakılmaz"
        }
    }
}