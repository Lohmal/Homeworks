package com.volkankivanc.tarsusogrencibilgisistemi.Ogretmen

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.AdapterView
import android.widget.ArrayAdapter
import android.widget.Toast
import com.google.firebase.auth.FirebaseAuth
import com.google.firebase.firestore.FirebaseFirestore
import com.volkankivanc.tarsusogrencibilgisistemi.R
import kotlinx.android.synthetic.main.fragment_ogretmen_not_islemleri.*



class OgretmenNotIslemleri : Fragment() {

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
                if (spinner != null){
                    val a = getActivity()?.let { ArrayAdapter(it, androidx.appcompat.R.layout.support_simple_spinner_dropdown_item,ab) }
                    spinner.adapter=a

                    spinner.onItemSelectedListener= object :AdapterView.OnItemSelectedListener{
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
                                               spinner2.adapter=c

                                               spinner2.onItemSelectedListener=object:AdapterView.OnItemSelectedListener{
                                                   override fun onItemSelected(
                                                       p0: AdapterView<*>?,
                                                       p1: View?,
                                                       p2: Int,
                                                       p3: Long
                                                   ) {

                                                       fStore.collection("Dersler").document(ab[p2].toString()).collection("Students").document(student2[p2]).get().addOnSuccessListener{ a ->
                                                           if (a != null) {

                                                               fStore.collection("Dersler").document(ab[p2].toString()).get().addOnSuccessListener{ b ->
                                                                   if (b != null) {
                                                                       dersadi_not_islemleri.text = "${b.get("DersAdi")}"
                                                                   }
                                                               }

                                                               val finalNotu = "${a.get("Final")}"
                                                               val vizeNotu = "${a.get("Vize")}"
                                                               val yidNotu = "${a.get("Yid")}"

                                                               vize_not.setText(vizeNotu)
                                                               yid_not.setText(yidNotu)
                                                               final_not.setText(finalNotu)

                                                               btnduzenle.setOnClickListener {
                                                                   duzenle(it,ab[p2].toString(),student2[p2])
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
        return inflater.inflate(R.layout.fragment_ogretmen_not_islemleri, container, false)
    }

    fun duzenle(view: View,a:String,b:String){

        val notlar =HashMap<String,Any>()
        notlar.put("Vize",vize_not.text.toString())
        notlar.put("Yid",yid_not.text.toString())
        notlar.put("Final",final_not.text.toString())

        fStore.collection("Dersler").document(a).collection("Students").document(b).update(notlar).addOnCompleteListener { a ->
            if (a.isSuccessful) {
                Toast.makeText(getActivity(),"Düzenlendi",Toast.LENGTH_SHORT).show()
            }
        }.addOnFailureListener {
            Toast.makeText(getActivity(),it.localizedMessage,Toast.LENGTH_SHORT).show()
        }
    }
}