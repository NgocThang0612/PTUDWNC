import './App.css';
import Navbar from './Components/Navbar';
import Sidebar from './Components/Sidebar';
import Footer from './Components/Footer';
import Layout from './Pages/Layout';
import Index from './Pages/Index';
import About from './Pages/About';
import Contact from './Pages/Contact';
import Rss from './Pages/Rss';
import AdminLayout from './Pages/Admin/Layout';
import * as AdminIndex from './Pages/Admin/Index';
import Authors from './Pages/Admin/Authors';
import Categories from './Pages/Admin/Categories';
import Comments from './Pages/Admin/Comments';
import Posts from './Pages/Admin/Post/Posts';
import Tags from './Pages/Admin/Tags';
import {
  BrowserRouter as Router,
  Routes,
  Route,
} from 'react-router-dom';
import Edit from './Pages/Admin/Post/Edit';

function App() {
  return (
    // <div>
    //   <Router>
    //     <Navbar />
    //     <div className='container-fluid'>
    //       <div className='row'>
    //         <div className='col-9'>
    //           <Routes>
    //             <Route path='/' element={<Layout />}>
    //               <Route path='/' element={<Index />} />
    //               <Route path='blog' element={<Index />} />
    //               <Route path='blog/Contact' element={<Contact />} />
    //               <Route path='blog/About' element={<About />} />
    //               <Route path='blog/Rss' element={<Rss />} />
    //             </Route>
    //           </Routes>
    //         </div>
    //         <div className='col-3 border-start'>
    //           <Sidebar />
    //         </div>
    //       </div>
    //     </div>
    //     <Footer />
    //   </Router>
    // </div>
    <Router>
      <Routes>
        <Route path='/' element={<Layout />}>
          <Route path='/' element={<Index />} />
          <Route path='blog' element={<Index />} />
          <Route path='blog/Contact' element={<Contact />} />
          <Route path='blog/About' element={<About />} />
          <Route path='blog/Rss' element={<Rss />} />
        </Route>
        <Route path='/admin' element={<AdminLayout />}>
          <Route path='/admin' element={<AdminIndex.default />} />
          <Route path='/admin/authors' element={<Authors />} />
          <Route path='/admin/categories' element={<Categories />} />
          <Route path='/admin/comments' element={<Comments />} />
          <Route path='/admin/posts' element={<Posts />} />
          <Route path='/admin/posts/edit' element={<Edit />} />
          <Route path='/admin/posts/edit/:id' element={<Edit />} />
          <Route path='/admin/tags' element={<Tags />} />
        </Route>
      </Routes>
      <Footer />
    </Router>
  );
}

export default App;
