package com.volkankivanc.tarsusogrencibilgisistemi.Giris

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import com.volkankivanc.tarsusogrencibilgisistemi.R
import kotlinx.android.synthetic.main.activity_giris3.*

class Giris3 : AppCompatActivity() {

    var text1:String? =null
    var text2:String? =null
    var text3:String? =null
    var text4:String? =null
    var text5:String? =null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_giris3)
    }

    fun act1(view: View){
        val intent= Intent(applicationContext, Giris1::class.java)
        startActivity(intent)
    }

    fun act2(view: View){
        val intent= Intent(applicationContext, Giris2::class.java)
        startActivity(intent)
    }

    fun getir(view: View){
        text1=editTextText1act3.text.toString()
        text2=editTextText2act3.text.toString()
        text3=editTextText3act3.text.toString()
        text4=editTextText4act3.text.toString()
        text5=editTextText5act3.text.toString()
        if(text1.isNullOrEmpty()){
            editTextText1act3.error = "Boş bırakılmaz"
        }
        if(text2.isNullOrEmpty()){
            editTextText2act3.error = "Boş bırakılmaz"
        }
        if(text3.isNullOrEmpty()){
            editTextText3act3.error = "Boş bırakılmaz"
        }
        if(text4.isNullOrEmpty()){
            editTextText4act3.error = "Boş bırakılmaz"
        }
        if(text5.isNullOrEmpty()){
            editTextText5act3.error = "Boş bırakılmaz"
        }

    }
}