import React, { useState, useEffect } from 'react';
import api from '../services/api';
import { formatLeadDate } from '../services/LeadServices';
import { MapPin, BriefcaseBusiness, PhoneCall, Mail  } from 'lucide-react';

export const CardLeadAccepted = () => {
  const [leads, setLeads] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetchLeads();
  }, []);

  async function fetchLeads() {
    setLoading(true);
    try {
      const response = await api.get('/api/Lead/accepted');
      setLeads(response.data);
    } catch (error) {
      console.error('Erro ao buscar leads:', error);
    } finally {
      setLoading(false);
    }
  }

  return (
    <>
      {loading ? (
        <div className="flex justify-center items-center h-32">
          <div className="w-8 h-8 border-4 border-gray-300 border-t-orange-500 rounded-full animate-spin"></div>
        </div>
      ) : (
        leads.map((lead) => (
          <div key={lead.id} className="flex flex-col w-full bg-white text-black mb-4 shadow-sm border border-gray-200">
            <div className="flex gap-3 p-3 items-center">
              <div className="bg-orange-500 rounded-full w-14 h-14 flex items-center justify-center text-white font-bold text-lg ml-4">
                {lead.fullName?.[0] || 'J'}
              </div>
              <div className="flex flex-col">
                <div className="font-bold">{lead.fullName}</div>
                <div className="text-gray-500">{formatLeadDate(lead.dateCreated)}</div>
              </div>
              
            </div>

            <div className="flex gap-3 p-4 border-b border-gray-200 text-gray-500 flex-wrap">
              <div className="text-lg flex items-center gap-1">
                <MapPin className="w-5 h-5" />
                {lead.suburb}
              </div>
              <div className="text-lg flex items-center gap-1">
                <BriefcaseBusiness className="w-5 h-5" />
                {lead.category}
              </div>
              <div className="text-lg flex items-center">Job ID: {lead.id}</div>
              <div className="flex flex-row items-center gap-2">
                <div className="flex flex-row">
                  <span className="text-lg text-gray-500">${lead.price}</span>
                  <p className="text-gray-500 text-lg ml-2">Lead Invitation</p>
                </div>
              </div>
            </div>
            <div className='flex flex-row p-4 gap-4'>
                <div className='flex flex-row items-center gap-2 text-orange-500'>
                <PhoneCall />
                {lead.phoneNumber}
                </div>
                <div className='flex flex-row items-center gap-2 text-orange-500'>
                <Mail />
                {lead.email}
                </div>
            </div>

            <div className="flex flex-row p-4 text-gray-500">
              <div>{lead.description}</div>
            </div>
          </div>
        ))
      )}
    </>
  );
};
