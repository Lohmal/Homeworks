package com.volkankivanc.tarsusogrencibilgisistemi.Ogrenci

import android.content.Intent
import android.graphics.Color
import android.graphics.drawable.ColorDrawable
import android.os.Bundle
import android.view.MenuItem
import android.widget.Toast
import androidx.appcompat.app.ActionBarDrawerToggle
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.GravityCompat
import androidx.drawerlayout.widget.DrawerLayout
import com.google.android.material.navigation.NavigationView
import com.google.firebase.auth.FirebaseAuth
import com.google.firebase.firestore.FirebaseFirestore
import com.volkankivanc.tarsusogrencibilgisistemi.Giris.Giris1
import com.volkankivanc.tarsusogrencibilgisistemi.R
import kotlinx.android.synthetic.main.header_menu.*


open class OgrenciActivity : AppCompatActivity() {
    private lateinit var drawerLayout: DrawerLayout
    private lateinit var navigationView: NavigationView

    private lateinit var actionBarDrawerToggle: ActionBarDrawerToggle
    private lateinit var auth: FirebaseAuth
    private lateinit var fStore : FirebaseFirestore

    override fun onOptionsItemSelected(item: MenuItem): Boolean {
        return if (actionBarDrawerToggle.onOptionsItemSelected(item)) {
            true
        } else super.onOptionsItemSelected(item)
    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_ogrenci)
        auth=FirebaseAuth.getInstance()
        fStore= FirebaseFirestore.getInstance()

        main_menu()

    }

    private fun main_menu() {
        val fragmentManager = supportFragmentManager
        val fragmentTransaction = fragmentManager.beginTransaction()
        val userid= auth.currentUser!!.uid

        val ogrenci_anasayfa = OgrenciAnasayfa()
        fragmentTransaction.replace(R.id.frameLayout, ogrenci_anasayfa).commit()
        supportActionBar?.setTitle("Anasayfa").toString()


        fStore.collection("Users").document(userid).get().addOnSuccessListener { document ->
            if (document != null){
                headertextname.text="${document.get("Ad")} ${document.get("Soyad")}"
            }
        }.addOnFailureListener { e ->
            Toast.makeText(this,e.localizedMessage,Toast.LENGTH_LONG).show()
        }

        drawerLayout = findViewById(R.id.drawer_layout)
        navigationView = findViewById(R.id.navigationView)
        actionBarDrawerToggle = ActionBarDrawerToggle(
            this, drawerLayout,
            R.string.menu_drawer_open,
            R.string.menu_drawer_close
        )
        drawerLayout.addDrawerListener(actionBarDrawerToggle)
        actionBarDrawerToggle.syncState()
        supportActionBar?.setDisplayHomeAsUpEnabled(true)
        navigationView.setNavigationItemSelectedListener(NavigationView.OnNavigationItemSelectedListener { item ->
            when (item.itemId) {
                R.id.nav_home -> {
                    val fragmentManager = supportFragmentManager
                    val fragmentTransaction = fragmentManager.beginTransaction()

                    val ogrenci_anasayfa = OgrenciAnasayfa()
                    fragmentTransaction.replace(R.id.frameLayout, ogrenci_anasayfa).commit()
                    supportActionBar?.setTitle("Anasayfa").toString()
                    drawerLayout.closeDrawer(GravityCompat.START)
                }
                R.id.nav_person -> {
                    val fragmentManager = supportFragmentManager
                    val fragmentTransaction = fragmentManager.beginTransaction()

                    val ogrenci_bilgileri = OgrenciPerson()
                    fragmentTransaction.replace(R.id.frameLayout, ogrenci_bilgileri).commit()
                    supportActionBar?.setTitle("Öğrenci Bilgileri").toString()
                    drawerLayout.closeDrawer(GravityCompat.START)
                }
                R.id.nav_kayit_islemleri -> {
                    Toast.makeText(getApplicationContext(), "Yapım Aşamasında", Toast.LENGTH_SHORT)
                        .show();
                    drawerLayout.closeDrawer(GravityCompat.START)
                }
                R.id.nav_not_islemleri -> {
                    val fragmentManager = supportFragmentManager
                    val fragmentTransaction = fragmentManager.beginTransaction()

                    val ogrenci_not = OgrenciNotIslemleri()
                    fragmentTransaction.replace(R.id.frameLayout, ogrenci_not).commit()
                    supportActionBar?.setTitle("Not İşlemleri").toString()
                    drawerLayout.closeDrawer(GravityCompat.START)
                }
                R.id.nav_sinav_talep_islemleri -> {
                    Toast.makeText(getApplicationContext(), "Yapım Aşamasında", Toast.LENGTH_SHORT)
                        .show();
                    drawerLayout.closeDrawer(GravityCompat.START)
                }
                R.id.nav_erasmus_basvuru -> {
                    Toast.makeText(getApplicationContext(), "Yapım Aşamasında", Toast.LENGTH_SHORT)
                        .show();
                    drawerLayout.closeDrawer(GravityCompat.START)
                }
                R.id.nav_harc -> {
                    Toast.makeText(getApplicationContext(), "Yapım Aşamasında", Toast.LENGTH_SHORT)
                        .show();
                    drawerLayout.closeDrawer(GravityCompat.START)
                }
                R.id.nav_basvuru -> {
                    Toast.makeText(getApplicationContext(), "Yapım Aşamasında", Toast.LENGTH_SHORT)
                        .show();
                    drawerLayout.closeDrawer(GravityCompat.START)
                }
                R.id.nav_logout -> {
                    auth.signOut()
                    val intent = Intent(this, Giris1::class.java)
                    startActivity(intent)
                    finish()
                }
            }
            true
        })
    }
}