package com.volkankivanc.tarsusogrencibilgisistemi.Ogretmen

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.AdapterView
import android.widget.ArrayAdapter
import android.widget.Button
import android.widget.Toast
import androidx.recyclerview.widget.LinearLayoutManager
import com.google.firebase.auth.FirebaseAuth
import com.google.firebase.firestore.FirebaseFirestore
import com.volkankivanc.tarsusogrencibilgisistemi.Adapter.OgrenciNotGoruntulemeAdapter
import com.volkankivanc.tarsusogrencibilgisistemi.R
import kotlinx.android.synthetic.main.fragment_ogrenci_not_islemleri.*
import kotlinx.android.synthetic.main.fragment_ogretmen_person.*
import kotlinx.android.synthetic.main.fragment_ogretmen_person.ad_soyad
import kotlinx.android.synthetic.main.fragment_ogretmen_person.ana_adi
import kotlinx.android.synthetic.main.fragment_ogretmen_person.baba_adi
import kotlinx.android.synthetic.main.fragment_ogretmen_person.birim
import kotlinx.android.synthetic.main.fragment_ogretmen_person.bolum
import kotlinx.android.synthetic.main.fragment_ogretmen_person.danisman
import kotlinx.android.synthetic.main.fragment_ogretmen_person.danisman_email
import kotlinx.android.synthetic.main.fragment_ogretmen_person.dogum_tarihi
import kotlinx.android.synthetic.main.fragment_ogretmen_person.dogum_yeri
import kotlinx.android.synthetic.main.fragment_ogretmen_person.ikincil_email
import kotlinx.android.synthetic.main.fragment_ogretmen_person.kurumsal_email
import kotlinx.android.synthetic.main.fragment_ogretmen_person.m_adi
import kotlinx.android.synthetic.main.fragment_ogretmen_person.ogr_no
import kotlinx.android.synthetic.main.fragment_ogretmen_person.prgrm
import kotlinx.android.synthetic.main.fragment_ogretmen_person.sinif
import kotlinx.android.synthetic.main.fragment_ogretmen_person.tc_no
import kotlinx.android.synthetic.main.fragment_ogretmen_person.tel_no
import kotlinx.android.synthetic.main.fragment_ogretmen_person.yariyil


class OgretmenPerson : Fragment() {

    private lateinit var fStore : FirebaseFirestore
    private lateinit var auth: FirebaseAuth

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        auth= FirebaseAuth.getInstance()
        fStore= FirebaseFirestore.getInstance()

        val userid= auth.currentUser!!.uid

        fStore.collection("Users").document(userid).get().addOnSuccessListener { document ->
            if (document != null){
                val student = ArrayList<String>()
                val student2 = ArrayList<String>()
                val ab=document.get("Ders") as ArrayList<*>
                if (spinner5 != null){
                    val a = getActivity()?.let { ArrayAdapter(it, androidx.appcompat.R.layout.support_simple_spinner_dropdown_item,ab) }
                    spinner5.adapter=a

                    spinner5.onItemSelectedListener= object : AdapterView.OnItemSelectedListener{
                        override fun onItemSelected(
                            p0: AdapterView<*>?,
                            p1: View?,
                            p2: Int,
                            p3: Long
                        ) {
                            fStore.collection("Dersler").document(ab[p2].toString()).collection("Students").get().addOnSuccessListener{ a ->
                                if (a != null) {
                                    student.clear()
                                    student2.clear()
                                    a.forEach {

                                        fStore.collection("Users").document(it.id).get().addOnSuccessListener { document ->
                                            if (document != null){

                                                var ad_soy="${document.get("Ad")} ${document.get("Soyad")}"
                                                student.add(ad_soy)
                                                student2.add(it.id)
                                                val c = getActivity()?.let { ArrayAdapter(it, androidx.appcompat.R.layout.support_simple_spinner_dropdown_item,student) }
                                                spinner6.adapter=c

                                                spinner6.onItemSelectedListener=object: AdapterView.OnItemSelectedListener{
                                                    override fun onItemSelected(
                                                        p0: AdapterView<*>?,
                                                        p1: View?,
                                                        p2: Int,
                                                        p3: Long
                                                    ) {
                                                        verial(student2[p2])
                                                    }

                                                    override fun onNothingSelected(p0: AdapterView<*>?) {
                                                        TODO("Not yet implemented")
                                                    }
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                        }
                        override fun onNothingSelected(p0: AdapterView<*>?) {
                            TODO("Not yet implemented")
                        }
                    }
                }
            }
        }

    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_ogretmen_person, container, false)
    }

    override fun onViewCreated(itemView: View, savedInstanceState: Bundle?) {
        super.onViewCreated(itemView, savedInstanceState)
        tc_ara.setOnClickListener {
            ara(it)
        }
    }

    fun verial(a:String?){
        fStore.collection("Users").document(a.toString()).get().addOnSuccessListener { document ->
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

    fun ara(view: View){
        fStore.collection("Users").whereEqualTo("tcNo",person_tc_ara.text.toString()).get().addOnSuccessListener { documents ->
            for (document in documents) {
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
        }.addOnFailureListener { exception ->
            Toast.makeText(getActivity(),exception.localizedMessage, Toast.LENGTH_SHORT).show()
            }

    }
}


