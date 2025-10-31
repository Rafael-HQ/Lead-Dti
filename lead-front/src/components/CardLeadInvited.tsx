import React, { useState, useEffect } from 'react';
import { MapPin, BriefcaseBusiness } from 'lucide-react';
import api from '../services/api';
import { formatLeadDate } from '../services/LeadServices';

const CardLeadInvited = () => {
  const [leads, setLeads] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);
  const [loadingAccept, setLoadingAccept] = useState<number | null>(null);
  const [notification, setNotification] = useState<string | null>(null);

  useEffect(() => {
    fetchLeads();
  }, []);

  async function fetchLeads() {
    setLoading(true);
    try {
      const response = await api.get('/api/Lead/invited');
      setLeads(response.data);
    } catch (error) {
      console.error('Erro ao buscar leads:', error);
    } finally {
      setLoading(false);
    }
  }

  async function acceptLead(leadId: number, leadName: string) {
    try {
      setLoadingAccept(leadId);
      await api.put(`/api/Lead/accept/${leadId}`);
      setLeads(prev => prev.filter(lead => lead.id !== leadId));

      setNotification(`Notificação enviada para vendas@test.com sobre o lead ${leadName}`);
      setTimeout(() => setNotification(null), 3000);
    } catch (error) {
      console.error('Erro ao aceitar lead:', error);
    } finally {
      setLoadingAccept(null);
    }
  }

  async function declineLead(leadId: number) {
    try {
      await api.put(`/api/Lead/decline/${leadId}`);
      setLeads(prev => prev.filter(lead => lead.id !== leadId));
      setNotification(`Lead recusado.`);
      setTimeout(() => setNotification(null), 3000);
    } catch (error) {
      console.error('Erro ao recusar lead:', error);
    }
  }



  return (
    <>
      {notification && (
        <div className="fixed bottom-4 right-4 bg-white border border-gray-300 shadow-lg rounded-md px-4 py-2 text-sm text-gray-800 z-50">
          {notification}
        </div>
      )}

      {loading ? (
        <div className="flex justify-center items-center h-32">
          <div className="w-8 h-8 border-4 border-gray-300 border-t-orange-500 rounded-full animate-spin"></div>
        </div>
      ) : leads.length === 0 ? (
        <div className="flex justify-center items-center h-32 text-gray-500 font-semibold">
          Não há nenhuma lead no momento.
        </div>
      ) : (
        leads.map((lead) => (
          <div key={lead.id} className="flex flex-col w-full bg-white text-black">
            <div className="flex gap-3 p-3">
              <div className="bg-orange-500 rounded-full w-14 h-14 flex items-center justify-center text-white font-bold text-lg ml-4">
                {lead.fristName?.[0] || 'J'}
              </div>
              <div className="flex flex-col">
                <div className="font-bold">{lead.fristName}</div>
                <div className="text-gray-500">{formatLeadDate(lead.dateCreated)}</div>
              </div>
            </div>

            <div className="flex gap-3 p-4 border-b-2 border-gray-200 text-gray-500">
              <div className="text-lg flex items-center gap-1">
                <MapPin className="w-5 h-5" />
                {lead.suburb}
              </div>
              <div className="text-lg flex items-center gap-1">
                <BriefcaseBusiness className="w-5 h-5" />
                {lead.category}
              </div>
              <div className="text-lg flex items-center">Job ID: {lead.id}</div>
            </div>

            <div className="flex flex-row p-4 border-b-2 border-gray-200 text-gray-500">
              <div>{lead.description}</div>
            </div>

            <div className="flex flex-row items-center gap-3 p-4">
              <div
                className="bg-orange-500 border-b-2 border-orange-900 cursor-pointer px-4 py-2 rounded-md text-white font-semibold flex items-center justify-center"
                onClick={() => acceptLead(lead.id, lead.fristName)}
              >
                {loadingAccept === lead.id ? (
                  <div className="w-5 h-5 border-2 border-white border-t-transparent rounded-full animate-spin"></div>
                ) : (
                  'Accept'
                )}
              </div>

              <div
                className="bg-gray-300 border-b-2 border-gray-800 cursor-pointer px-4 py-2 rounded-md font-semibold"
                onClick={() => declineLead(lead.id)}
              >
                Decline
              </div>

              <div className="flex flex-row items-center gap-2">
                <div className="flex flex-row">
                  <span className="text-lg font-semibold text-black">${lead.price}</span>
                  <p className="text-gray-500 text-lg ml-2">Lead Invitation</p>
                </div>
              </div>
            </div>
          </div>
        ))
      )}
    </>
  );
};

export default CardLeadInvited;
