package com.volkankivanc.tarsusogrencibilgisistemi.Ogretmen

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import com.google.firebase.auth.FirebaseAuth
import com.google.firebase.firestore.FirebaseFirestore
import com.volkankivanc.tarsusogrencibilgisistemi.R

import kotlinx.android.synthetic.main.fragment_ogretmen_anasayfa.*


class OgretmenAnasayfa : Fragment() {
    private lateinit var fStore : FirebaseFirestore
    private lateinit var auth: FirebaseAuth

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        auth= FirebaseAuth.getInstance()
        fStore= FirebaseFirestore.getInstance()

        val userid= auth.currentUser!!.uid

        fStore.collection("Users").document(userid).get().addOnSuccessListener { document ->
            if (document != null){
                ogr_tc_no.text="${document.get("tcNo")}"
                ogr_ad_soyad.text="${document.get("Ad")} ${document.get("Soyad")}"
                ogr_dogum_tarihi.text="${document.get("dg_tarihi")}"
                ogr_kurumsal_email.text="${document.get("Email")}"
                ogr_tel_no.text="${document.get("TelNo")}"
                ogr_dogum_yeri.text="${document.get("dg_yeri")}"
            }
        }
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_ogretmen_anasayfa, container, false)
    }
}