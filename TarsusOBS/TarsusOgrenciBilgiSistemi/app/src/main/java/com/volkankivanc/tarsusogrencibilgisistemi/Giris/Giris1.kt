package com.volkankivanc.tarsusogrencibilgisistemi.Giris

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.widget.Toast
import com.google.firebase.auth.FirebaseAuth
import com.google.firebase.firestore.FirebaseFirestore
import com.volkankivanc.tarsusogrencibilgisistemi.Ogrenci.OgrenciActivity
import com.volkankivanc.tarsusogrencibilgisistemi.Ogretmen.OgretmenActivity
import com.volkankivanc.tarsusogrencibilgisistemi.R
import kotlinx.android.synthetic.main.activity_main.*

class Giris1 : AppCompatActivity() {
    var emailText:String? =null
    var passwordText:String? =null
    private lateinit var auth: FirebaseAuth
    private lateinit var fStore : FirebaseFirestore

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)


        auth=FirebaseAuth.getInstance()
        fStore= FirebaseFirestore.getInstance()


        val guncelkullanici=auth.currentUser
        //if (hatirla.isChecked)
        if (guncelkullanici != null){
            val userid= auth.currentUser!!.uid
            fStore.collection("Users").document(userid).get().addOnSuccessListener { document ->
                if (document != null){
                    val is_teacher=document.get("is_teacher") as Boolean

                    if (!is_teacher){
                        val intent=Intent(this, OgrenciActivity::class.java)
                        startActivity(intent)
                        finish()
                    }
                    else if (is_teacher){
                        val intent=Intent(this, OgretmenActivity::class.java)
                        startActivity(intent)
                        finish()
                    }
                    else{
                        Toast.makeText(this,"Giriş Yapılamadı", Toast.LENGTH_SHORT).show()
                    }
                }
            }
        }
    }
    fun act2(view: View){
        val intent= Intent(applicationContext, Giris2::class.java)
        startActivity(intent)
    }
    fun act3(view: View){
        val intent= Intent(applicationContext, Giris3::class.java)
        startActivity(intent)
    }
    fun giris(view: View){
        emailText= editTextTextEmailAddress.text.toString()
        passwordText=editTextTextPassword.text.toString()
        if(emailText.isNullOrEmpty()){
            editTextTextEmailAddress.error = "Boş bırakılmaz"
        }
        if(passwordText.isNullOrEmpty()){
            editTextTextPassword.error = "Boş bırakılmaz"
        }
        else{
            auth.signInWithEmailAndPassword(emailText!!,passwordText!!).addOnCompleteListener { task ->

                if (task.isSuccessful){
                    val userid= auth.currentUser!!.uid
                    fStore.collection("Users").document(userid).get().addOnSuccessListener { document ->
                        if (document != null){
                            val is_teacher=document.get("is_teacher") as Boolean

                            if (!is_teacher){
                                val intent=Intent(this, OgrenciActivity::class.java)
                                startActivity(intent)
                                finish()
                            }
                            else if (is_teacher){
                                val intent=Intent(this, OgretmenActivity::class.java)
                                startActivity(intent)
                                finish()
                            }
                            else{
                                Toast.makeText(this,"Giriş Yapılamadı", Toast.LENGTH_SHORT).show()
                            }
                        }
                    }
                }
            }.addOnFailureListener { exception ->
                Toast.makeText(this,exception.localizedMessage, Toast.LENGTH_SHORT).show()
            }
        }
    }
}