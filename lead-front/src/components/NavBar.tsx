import React from 'react'

interface NavBarProps {
  activeTab: 'invited' | 'accepted';
  onTabChange: (tab: 'invited' | 'accepted') => void;
}

const NavBar: React.FC<NavBarProps> = ({ activeTab, onTabChange }) => {
    return (
        <nav className='flex gap-1 bg-gray-200 w-full'>
            <div 
                className={`flex p-4 cursor-pointer bg-white text-black w-1/2 justify-center font-semibold ${
                    activeTab === 'invited' ? 'border-b-4 border-orange-500' : 'border-b-2 border-gray-300'
                }`}
                onClick={() => onTabChange('invited')}
            >
                Invited
            </div>
            <div 
                className={`flex p-4 cursor-pointer bg-white text-black w-1/2 justify-center font-semibold ${
                    activeTab === 'accepted' ? 'border-b-4 border-orange-500' : 'border-b-2 border-gray-300'
                }`}
                onClick={() => onTabChange('accepted')}
            >
                Accepted
            </div>
        </nav>
    )
}

export default NavBar