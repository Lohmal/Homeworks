package com.volkankivanc.tarsusogrencibilgisistemi.Ogrenci

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.recyclerview.widget.LinearLayoutManager
import com.google.firebase.auth.FirebaseAuth
import com.google.firebase.firestore.FirebaseFirestore
import com.volkankivanc.tarsusogrencibilgisistemi.Adapter.OgrenciDevamsizlikGoruntulemeAdapter
import com.volkankivanc.tarsusogrencibilgisistemi.Model.DevamsizlikGoruntuleme
import com.volkankivanc.tarsusogrencibilgisistemi.R
import kotlinx.android.synthetic.main.fragment_ogrenci_person.*


class OgrenciPerson : Fragment() {
    var devamlistesi = ArrayList<DevamsizlikGoruntuleme>()
    private lateinit var auth: FirebaseAuth
    private lateinit var fStore : FirebaseFirestore
    private lateinit var recylerViewAdapterr: OgrenciDevamsizlikGoruntulemeAdapter

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_ogrenci_person, container, false)
    }

    override fun onViewCreated(itemView: View, savedInstanceState: Bundle?) {
        super.onViewCreated(itemView, savedInstanceState)
        recycler_view_person.apply {

            Verial()
            var layoutManagerr = LinearLayoutManager(activity)
            recycler_view_person.layoutManager=layoutManagerr
            recylerViewAdapterr = OgrenciDevamsizlikGoruntulemeAdapter(devamlistesi)
            recycler_view_person.adapter=recylerViewAdapterr
        }
    }
    private fun Verial() {
        auth=FirebaseAuth.getInstance()
        fStore= FirebaseFirestore.getInstance()

        val userid = auth.currentUser!!.uid

        fStore.collection("Dersler").addSnapshotListener { snapshot, error ->
            if (error != null) {
                Toast.makeText(getActivity(), error.localizedMessage, Toast.LENGTH_SHORT).show()
            } else {
                if (snapshot != null) {
                    if (!snapshot.isEmpty) {
                        val documents = snapshot.documents
                        devamlistesi.clear()
                        for (documentt in documents) {

                            val Dersadi = "${documentt.get("DersAdi")}"

                            fStore.collection("Dersler").document(documentt.id).collection("Students").document(userid).get().addOnSuccessListener{ a ->
                                if (a != null) {
                                    val t_dev = "${a.get("Teorik_devamsizlik")}"
                                    val uyg_deva = "${a.get("Uyg_devamsizlik")}"

                                    var devamsizlik =
                                        DevamsizlikGoruntuleme(Dersadi, t_dev, uyg_deva)
                                    devamlistesi.add(devamsizlik)
                                    recylerViewAdapterr.notifyDataSetChanged()
                                }
                            }

                        }
                        recylerViewAdapterr.notifyDataSetChanged()
                    }
                }
            }
        }
    }


}