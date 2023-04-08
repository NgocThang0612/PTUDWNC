import React from 'react'
import ContactAddress from '../Components/Contacts/ContactAddress';
import ContactForm from '../Components/Contacts/ContactForm';

const Contact = () => {
  return (
    <div>
      <div className='bg-success mb-5 p-5 text-center'>
        <h1 className='text-info'>Thông tin liên hệ</h1>
        <p className='text-white'>
          Cảm ơn đã ghé thăm Tat-Blog
        </p>
      </div>

      <div className="row">
        <div className="col-7">
          <ContactForm />
        </div>

        <div className="col-5">
          <ContactAddress />
        </div>
      </div>
    </div>
  )
}

export default Contact;