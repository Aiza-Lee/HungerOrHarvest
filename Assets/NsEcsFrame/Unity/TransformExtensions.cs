using System;
using UnityEngine;
using NsEcsFrame.Core;
using NsEcsFrame.Components;
using System.Collections.Generic;

namespace NsEcsFrame.Unity
{
    /// <summary>
    /// Unity平台下的数据结构扩展，提供高精度向量和四元数
    /// </summary>
    
    /// <summary>
    /// 高精度三维向量
    /// </summary>
    [Serializable]
    public struct Vector3d
    {
        public double x;
        public double y;
        public double z;
        
        public Vector3d(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        
        public Vector3d(Vector3 vector)
        {
            this.x = vector.x;
            this.y = vector.y;
            this.z = vector.z;
        }
        
        public static Vector3d zero => new Vector3d(0, 0, 0);
        public static Vector3d one => new Vector3d(1, 1, 1);
        public static Vector3d up => new Vector3d(0, 1, 0);
        public static Vector3d down => new Vector3d(0, -1, 0);
        public static Vector3d left => new Vector3d(-1, 0, 0);
        public static Vector3d right => new Vector3d(1, 0, 0);
        public static Vector3d forward => new Vector3d(0, 0, 1);
        public static Vector3d back => new Vector3d(0, 0, -1);
        
        public static implicit operator Vector3(Vector3d v) => new Vector3((float)v.x, (float)v.y, (float)v.z);
        
        public static Vector3d operator +(Vector3d a, Vector3d b) => new Vector3d(a.x + b.x, a.y + b.y, a.z + b.z);
        public static Vector3d operator -(Vector3d a, Vector3d b) => new Vector3d(a.x - b.x, a.y - b.y, a.z - b.z);
        public static Vector3d operator *(Vector3d a, double d) => new Vector3d(a.x * d, a.y * d, a.z * d);
        public static Vector3d operator /(Vector3d a, double d) => new Vector3d(a.x / d, a.y / d, a.z / d);
        
        public override string ToString() => $"({x}, {y}, {z})";
    }
    
    /// <summary>
    /// 高精度四元数
    /// </summary>
    [Serializable]
    public struct Quaterniond
    {
        public double x;
        public double y;
        public double z;
        public double w;
        
        public Quaterniond(double x, double y, double z, double w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
        
        public Quaterniond(Quaternion quaternion)
        {
            this.x = quaternion.x;
            this.y = quaternion.y;
            this.z = quaternion.z;
            this.w = quaternion.w;
        }
        
        public static Quaterniond identity => new Quaterniond(0, 0, 0, 1);
        
        public static implicit operator Quaternion(Quaterniond q) => new Quaternion((float)q.x, (float)q.y, (float)q.z, (float)q.w);
        
        public override string ToString() => $"({x}, {y}, {z}, {w})";
    }
    
    /// <summary>
    /// Transform组件，存储实体的位置、旋转和缩放
    /// </summary>
    public class TransformComponent : IComponent
    {
        /// <summary>
        /// 世界坐标位置
        /// </summary>
        public Vector3d position = Vector3d.zero;
        
        /// <summary>
        /// 旋转
        /// </summary>
        public Quaterniond rotation = Quaterniond.identity;
        
        /// <summary>
        /// 缩放
        /// </summary>
        public Vector3d scale = Vector3d.one;
        
        /// <summary>
        /// 父实体ID
        /// </summary>
        public EntityId parent = EntityId.NullEntityId;
        
        /// <summary>
        /// 子实体ID列表
        /// </summary>
        public List<EntityId> children = new();
        
        /// <summary>
        /// 局部坐标系位置
        /// </summary>
        public Vector3d localPosition = Vector3d.zero;
        
        /// <summary>
        /// 局部坐标系旋转
        /// </summary>
        public Quaterniond localRotation = Quaterniond.identity;
        
        /// <summary>
        /// 局部坐标系缩放
        /// </summary>
        public Vector3d localScale = Vector3d.one;
        
        /// <summary>
        /// 右方向向量
        /// </summary>
        public Vector3d Right
        {
            get
            {
                // 将四元数转换为3x3矩阵的右方向向量
                double x2 = rotation.x * 2.0;
                double y2 = rotation.y * 2.0;
                double z2 = rotation.z * 2.0;
                double xx = rotation.x * x2;
                double xy = rotation.x * y2;
                double xz = rotation.x * z2;
                double yy = rotation.y * y2;
                double yz = rotation.y * z2;
                double zz = rotation.z * z2;
                double wx = rotation.w * x2;
                double wy = rotation.w * y2;
                double wz = rotation.w * z2;

                return new Vector3d(
                    1.0 - (yy + zz),
                    xy + wz,
                    xz - wy
                );
            }
        }
        
        /// <summary>
        /// 上方向向量
        /// </summary>
        public Vector3d Up
        {
            get
            {
                // 将四元数转换为3x3矩阵的上方向向量
                double x2 = rotation.x * 2.0;
                double y2 = rotation.y * 2.0;
                double z2 = rotation.z * 2.0;
                double xx = rotation.x * x2;
                double xy = rotation.x * y2;
                double xz = rotation.x * z2;
                double yy = rotation.y * y2;
                double yz = rotation.y * z2;
                double zz = rotation.z * z2;
                double wx = rotation.w * x2;
                double wy = rotation.w * y2;
                double wz = rotation.w * z2;

                return new Vector3d(
                    xy - wz,
                    1.0 - (xx + zz),
                    yz + wx
                );
            }
        }
        
        /// <summary>
        /// 前方向向量
        /// </summary>
        public Vector3d Forward
        {
            get
            {
                // 将四元数转换为3x3矩阵的前方向向量
                double x2 = rotation.x * 2.0;
                double y2 = rotation.y * 2.0;
                double z2 = rotation.z * 2.0;
                double xx = rotation.x * x2;
                double xy = rotation.x * y2;
                double xz = rotation.x * z2;
                double yy = rotation.y * y2;
                double yz = rotation.y * z2;
                double zz = rotation.z * z2;
                double wx = rotation.w * x2;
                double wy = rotation.w * y2;
                double wz = rotation.w * z2;

                return new Vector3d(
                    xz + wy,
                    yz - wx,
                    1.0 - (xx + yy)
                );
            }
        }
    }
}
