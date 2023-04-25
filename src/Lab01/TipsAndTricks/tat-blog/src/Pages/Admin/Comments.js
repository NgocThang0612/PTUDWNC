import React, { useEffect, useState } from 'react';
import Table from 'react-bootstrap/Table';
import { Link, useParams } from 'react-router-dom';
import { getComments } from '../../Services/BlogReponsitory';
import Loading from '../../Components/Loading';

const Categories = () => {
    const [commentsList, setCommentsList] = useState([]),
        [isVisibleLoading, setIsVisibleLoading] = useState(true);

        useEffect(() => {
            document.title = 'Danh sách bình luận';
         
            getComments().then(data => {
                    if (data)
                        setCommentsList(data);
                    else
                        setCommentsList([]);
                    setIsVisibleLoading(false);
                });
        }, []);

    return (
        <>
            <h1>Danh sách bình luận </h1>
            {isVisibleLoading ? <Loading /> :
                <Table striped responsive bordered>
                    <thead>
                        <tr>
                            <th>Người bình luận</th>
                            <th>Nội dung</th>
                            <th>Ngày bình luận</th>
                            <th>Trạng thái</th>
                        </tr>
                    </thead>
                    <tbody>
                        {commentsList.length > 0 ? commentsList.map((items, index) =>
                            <tr key={index}>
                                <td>
                                    <Link
                                        to={`/admin/comments/${items.id}`}
                                        className='text-bold'>
                                        {items.fullName}
                                    </Link>
                                </td>
                                <td>{items.description}</td>
                                <td>{items.joinedDate}</td>
                                <td>{items.approved}</td> 
                            </tr>
                        ) :
                            <tr>
                                <td colSpan={4}>
                                    <h4 className='text-danger text-center'>Không tìm thấy bình luận nào</h4>
                                </td>
                            </tr>}
                    </tbody>
                </Table>
            }
        </>
    );
}


export default Categories;