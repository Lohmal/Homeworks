package com.volkankivanc.tarsusogrencibilgisistemi.Ogrenci

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import com.google.firebase.auth.FirebaseAuth
import com.google.firebase.firestore.FirebaseFirestore
import com.volkankivanc.tarsusogrencibilgisistemi.R
import kotlinx.android.synthetic.main.fragment_ogrenci_anasayfa.*


class OgrenciAnasayfa : Fragment() {
    private lateinit var fStore : FirebaseFirestore
    private lateinit var auth: FirebaseAuth

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        auth= FirebaseAuth.getInstance()
        fStore= FirebaseFirestore.getInstance()

        val userid= auth.currentUser!!.uid

        fStore.collection("Users").document(userid).get().addOnSuccessListener { document ->
            if (document != null){
                ogr_no.text="${document.get("OkulNo")}"
                tc_no.text="${document.get("tcNo")}"
                ad_soyad.text="${document.get("Ad")} ${document.get("Soyad")}"
                dogum_tarihi.text="${document.get("dg_tarihi")}"
                ana_adi.text="${document.get("A_adi")}"
                baba_adi.text="${document.get("B_adi")}"
                sinif.text="${document.get("Sinif")}"
                yariyil.text="${document.get("Yariyil")}"
                kurumsal_email.text="${document.get("kurumsal_email")}"
                ikincil_email.text="${document.get("ikincil_email")}"
                danisman.text="${document.get("Danisman")}"
                danisman_email.text="${document.get("Danisman_email")}"
                tel_no.text="${document.get("TelNo")}"
                dogum_yeri.text="${document.get("dg_yeri")}"
                birim.text="${document.get("Birim")}"
                bolum.text="${document.get("Bolum")}"
                prgrm.text="${document.get("Program")}"
                m_adi.text="${document.get("Mufredat_adi")}"
            }
        }

    }


    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_ogrenci_anasayfa, container, false)
    }

}