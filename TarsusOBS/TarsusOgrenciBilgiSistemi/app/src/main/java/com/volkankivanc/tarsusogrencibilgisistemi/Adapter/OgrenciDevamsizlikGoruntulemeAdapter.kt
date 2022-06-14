package com.volkankivanc.tarsusogrencibilgisistemi.Adapter

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.volkankivanc.tarsusogrencibilgisistemi.Model.DevamsizlikGoruntuleme
import com.volkankivanc.tarsusogrencibilgisistemi.R

import kotlinx.android.synthetic.main.recycler_row_person.view.*


class OgrenciDevamsizlikGoruntulemeAdapter(val devamlistesi:ArrayList<DevamsizlikGoruntuleme>): RecyclerView.Adapter<OgrenciDevamsizlikGoruntulemeAdapter.DevamsizlikGoruntulemeHolder>() {

    class DevamsizlikGoruntulemeHolder(itemView: View): RecyclerView.ViewHolder(itemView) {

    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): DevamsizlikGoruntulemeHolder {
        val inflater = LayoutInflater.from(parent.context)
        val view = inflater.inflate(R.layout.recycler_row_person,parent,false)
        return DevamsizlikGoruntulemeHolder(view)
    }

    override fun onBindViewHolder(holder: DevamsizlikGoruntulemeHolder, position: Int) {
        holder.itemView.dersadi_person.text=devamlistesi[position].dev_dersadi
        holder.itemView.teorik_dev.text=devamlistesi[position].teorik
        holder.itemView.uyg_dev.text=devamlistesi[position].uyg
    }

    override fun getItemCount(): Int {
        return devamlistesi.size
    }
}