import React, { useEffect, useState } from 'react';
import Table from 'react-bootstrap/Table';
import { Link, useParams } from 'react-router-dom';
import { getTags } from '../../Services/BlogReponsitory';
import Loading from '../../Components/Loading';

const Categories = () => {
    const [tagsList, setTagsList] = useState([]),
        [isVisibleLoading, setIsVisibleLoading] = useState(true);

        useEffect(() => {
            document.title = 'Danh sách các thẻ';
         
            getTags().then(data => {
                    if (data)
                        setTagsList(data);
                    else
                        setTagsList([]);
                    setIsVisibleLoading(false);
                });
        }, []);

    return (
        <>
            <h1>Danh sách các thẻ </h1>
            {isVisibleLoading ? <Loading /> :
                <Table striped responsive bordered>
                    <thead>
                        <tr>
                            <th>Tên thẻ</th>
                            <th>Nội dung</th>
                            <th>Slug</th>
                            <th>Số bài viết</th>
                        </tr>
                    </thead>
                    <tbody>
                        {tagsList.length > 0 ? tagsList.map((items, index) =>
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
                                    <h4 className='text-danger text-center'>Không tìm thấy thẻ nào</h4>
                                </td>
                            </tr>}
                    </tbody>
                </Table>
            }
        </>
    );
}


export default Categories;