import React from 'react';

const Sidebar =(props)=>{

    const menuItems=["Accounts","Contacts","Sales Orders"];
    const renderMenuItems = menuItems.map((item)=>
        <li>{item}</li>
    );
                

    return(

        <div>
            <ul>
                {renderMenuItems}
            </ul>
        </div>
    );
};


export default Sidebar;