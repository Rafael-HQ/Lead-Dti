import './App.css'
import CardLeadInvited from './components/CardLeadInvited'
import { CardLeadAccepted } from './components/CardLeadAccepted'
import NavBar from './components/NavBar'
import { useState } from 'react'

function App() {
  const [activeTab, setActiveTab] = useState<'invited' | 'accepted'>('invited')

  const handleTabChange = (tab: 'invited' | 'accepted') => {
    setActiveTab(tab)
  }

  return (
    <div className="flex flex-col items-center h-screen w-screen p-3 gap-4">
      <NavBar activeTab={activeTab} onTabChange={handleTabChange} />
      
      {activeTab === 'invited' && <CardLeadInvited />}
      {activeTab === 'accepted' && <CardLeadAccepted />}
    </div>
  )
}

export default App
