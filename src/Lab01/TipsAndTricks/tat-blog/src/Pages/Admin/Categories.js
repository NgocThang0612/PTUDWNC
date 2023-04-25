import React, { useEffect, useState } from 'react';
import Table from 'react-bootstrap/Table';
import { Link, useParams } from 'react-router-dom';
import { getCategories } from '../../Services/BlogReponsitory';
import Loading from '../../Components/Loading';

const Categories = () => {
    const [categoriesList, setCategoriesList] = useState([]),
        [isVisibleLoading, setIsVisibleLoading] = useState(true);

        useEffect(() => {
            document.title = 'Danh sách chủ đề';
         
            getCategories().then(data => {
                    if (data)
                        setCategoriesList(data);
                    else
                        setCategoriesList([]);
                    setIsVisibleLoading(false);
                });
        }, []);

    return (
        <>
            <h1>Danh sách chủ đề </h1>
            {isVisibleLoading ? <Loading /> :
                <Table striped responsive bordered>
                    <thead>
                        <tr>
                            <th>Tên chủ đề</th>
                            <th>Nội dung</th>
                            <th>Slug</th>
                            <th>Số bài viết</th>
                        </tr>
                    </thead>
                    <tbody>
                        {categoriesList.length > 0 ? categoriesList.map((items, index) =>
                            <tr key={index}>
                                <td>
                                    <Link
                                        to={`/admin/categories/${items.id}`}
                                        className='text-bold'>
                                        {items.name}
                                    </Link>
                                </td>
                                <td>{items.description}</td>
                                <td>{items.urlSlug}</td>
                                <td>{items.postCount}</td> 
                            </tr>
                        ) :
                            <tr>
                                <td colSpan={4}>
                                    <h4 className='text-danger text-center'>Không tìm thấy chủ đề nào</h4>
                                </td>
                            </tr>}
                    </tbody>
                </Table>
            }
        </>
    );
}


export default Categories;