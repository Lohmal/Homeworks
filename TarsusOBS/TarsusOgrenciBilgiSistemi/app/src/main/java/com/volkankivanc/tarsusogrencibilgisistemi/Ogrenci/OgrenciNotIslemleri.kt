package com.volkankivanc.tarsusogrencibilgisistemi.Ogrenci

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.google.firebase.auth.FirebaseAuth
import com.google.firebase.firestore.FirebaseFirestore
import com.volkankivanc.tarsusogrencibilgisistemi.Adapter.OgrenciNotGoruntulemeAdapter

import com.volkankivanc.tarsusogrencibilgisistemi.Model.NotGrountuleme
import com.volkankivanc.tarsusogrencibilgisistemi.R
import kotlinx.android.synthetic.main.fragment_ogrenci_anasayfa.*
import kotlinx.android.synthetic.main.fragment_ogrenci_not_islemleri.*


class OgrenciNotIslemleri : Fragment() {

    var notlistesi = ArrayList<NotGrountuleme>()
    private lateinit var auth: FirebaseAuth
    private lateinit var fStore : FirebaseFirestore
    private lateinit var recylerViewAdapter: OgrenciNotGoruntulemeAdapter

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_ogrenci_not_islemleri, container, false)
    }

    override fun onViewCreated(itemView: View, savedInstanceState: Bundle?) {
        super.onViewCreated(itemView, savedInstanceState)
        recycler_view.apply {

            Verial()
            var layoutManagerr = LinearLayoutManager(activity)
            recycler_view.layoutManager=layoutManagerr
            recylerViewAdapter = OgrenciNotGoruntulemeAdapter(notlistesi)
            recycler_view.adapter=recylerViewAdapter
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
                        notlistesi.clear()
                        for (documentt in documents) {

                            val Dersadi = "${documentt.get("DersAdi")}"

                            fStore.collection("Dersler").document(documentt.id).collection("Students").document(userid).get().addOnSuccessListener{ a ->
                                    if (a != null) {
                                        val finalNotu = "${a.get("Final")}"
                                        val vizeNotu = "${a.get("Vize")}"
                                        val yidNotu = "${a.get("Yid")}"

                                        var Notlar =
                                            NotGrountuleme(Dersadi, yidNotu, vizeNotu, finalNotu)
                                        notlistesi.add(Notlar)
                                        recylerViewAdapter.notifyDataSetChanged()
                                    }
                                }

                        }
                        recylerViewAdapter.notifyDataSetChanged()
                    }
                }
            }
        }
    }
}