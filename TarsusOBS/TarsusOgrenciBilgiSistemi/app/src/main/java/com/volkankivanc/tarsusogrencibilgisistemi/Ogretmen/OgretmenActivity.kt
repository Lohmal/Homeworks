package com.volkankivanc.tarsusogrencibilgisistemi.Ogretmen

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.MenuItem
import android.widget.Toast
import androidx.appcompat.app.ActionBarDrawerToggle
import androidx.core.view.GravityCompat
import androidx.drawerlayout.widget.DrawerLayout
import com.google.android.material.navigation.NavigationView
import com.google.firebase.auth.FirebaseAuth
import com.google.firebase.firestore.FirebaseFirestore
import com.volkankivanc.tarsusogrencibilgisistemi.Giris.Giris1
import com.volkankivanc.tarsusogrencibilgisistemi.Ogrenci.OgrenciAnasayfa
import com.volkankivanc.tarsusogrencibilgisistemi.Ogrenci.OgrenciNotIslemleri
import com.volkankivanc.tarsusogrencibilgisistemi.Ogrenci.OgrenciPerson
import com.volkankivanc.tarsusogrencibilgisistemi.R
import kotlinx.android.synthetic.main.header_menu.*

class OgretmenActivity : AppCompatActivity() {

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
        setContentView(R.layout.activity_ogretmen)
        auth= FirebaseAuth.getInstance()
        fStore= FirebaseFirestore.getInstance()
        main_menu()
    }

    private fun main_menu() {
        val fragmentManager = supportFragmentManager
        val fragmentTransaction = fragmentManager.beginTransaction()
        val userid= auth.currentUser!!.uid

        val ogretmen_anasayfa =OgretmenAnasayfa()
        fragmentTransaction.replace(R.id.ogr_frameLayout, ogretmen_anasayfa).commit()
        supportActionBar?.setTitle("Anasayfa").toString()


        fStore.collection("Users").document(userid).get().addOnSuccessListener { document ->
            if (document != null){
                headertextname.text="${document.get("Ad")} ${document.get("Soyad")}"
            }
        }.addOnFailureListener { e ->
            Toast.makeText(this,e.localizedMessage, Toast.LENGTH_LONG).show()
        }

        drawerLayout = findViewById(R.id.ogr_drawer_layout)
        navigationView = findViewById(R.id.ogr_navigationView)
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
                R.id.nav_ogr_home-> {
                    val fragmentManager = supportFragmentManager
                    val fragmentTransaction = fragmentManager.beginTransaction()

                    val ogretmen_anasayfa =OgretmenAnasayfa()
                    fragmentTransaction.replace(R.id.ogr_frameLayout, ogretmen_anasayfa).commit()
                    supportActionBar?.setTitle("Anasayfa").toString()
                    drawerLayout.closeDrawer(GravityCompat.START)
                }
                R.id.nav_ders_islemleri-> {
                    val fragmentManager = supportFragmentManager
                    val fragmentTransaction = fragmentManager.beginTransaction()

                    val ogretmen_ders =OgretmenDers()
                    fragmentTransaction.replace(R.id.ogr_frameLayout, ogretmen_ders).commit()
                    supportActionBar?.setTitle("Ders İşlemleri").toString()
                    drawerLayout.closeDrawer(GravityCompat.START)
                }
                R.id.nav_ogrenci_islemleri -> {
                    val fragmentManager = supportFragmentManager
                    val fragmentTransaction = fragmentManager.beginTransaction()

                    val ogretmen_person =OgretmenPerson()
                    fragmentTransaction.replace(R.id.ogr_frameLayout, ogretmen_person).commit()
                    supportActionBar?.setTitle("Öğrenci İşlemleri").toString()
                    drawerLayout.closeDrawer(GravityCompat.START)
                }
                R.id.nav_akademisyen_islemleri -> {
                    Toast.makeText(getApplicationContext(), "Yapım Aşamasında", Toast.LENGTH_SHORT)
                        .show();
                    drawerLayout.closeDrawer(GravityCompat.START)
                }
                R.id.nav_bologna_islemleri -> {
                    Toast.makeText(getApplicationContext(), "Yapım Aşamasında", Toast.LENGTH_SHORT)
                        .show();
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

                    val ogretmen_not_islemleri =OgretmenNotIslemleri()
                    fragmentTransaction.replace(R.id.ogr_frameLayout, ogretmen_not_islemleri).commit()
                    supportActionBar?.setTitle("Not İşlemleri").toString()
                    drawerLayout.closeDrawer(GravityCompat.START)
                }
                R.id.nav_belgeler-> {
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
