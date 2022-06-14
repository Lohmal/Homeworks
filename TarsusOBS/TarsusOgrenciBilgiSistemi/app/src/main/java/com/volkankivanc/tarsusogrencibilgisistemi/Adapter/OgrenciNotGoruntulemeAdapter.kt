package com.volkankivanc.tarsusogrencibilgisistemi.Adapter

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.volkankivanc.tarsusogrencibilgisistemi.Model.NotGrountuleme
import com.volkankivanc.tarsusogrencibilgisistemi.R
import kotlinx.android.synthetic.main.recycler_row_notlar.view.*


class OgrenciNotGoruntulemeAdapter(val notlistesi:ArrayList<NotGrountuleme>): RecyclerView.Adapter<OgrenciNotGoruntulemeAdapter.NotGrountulemeHolder>() {

    class NotGrountulemeHolder(itemView: View): RecyclerView.ViewHolder(itemView) {

    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): NotGrountulemeHolder {
        val inflater = LayoutInflater.from(parent.context)
        val view = inflater.inflate(R.layout.recycler_row_notlar,parent,false)
        return NotGrountulemeHolder(view)
    }

    override fun onBindViewHolder(holder: NotGrountulemeHolder, position: Int) {
        holder.itemView.dersadi.text=notlistesi[position].dersadi
        holder.itemView.vize.text=notlistesi[position].vizenout
        holder.itemView.Final.text=notlistesi[position].finalnotu
        holder.itemView.yid.text=notlistesi[position].Yidnotu
    }

    override fun getItemCount(): Int {
        return notlistesi.size
    }


}