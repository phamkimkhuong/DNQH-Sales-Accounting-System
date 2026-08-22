using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DNQH_KeToanBanHang.Helpers
{
    public enum UiIconType
    {
        Dashboard,
        System,
        Key,
        Shield,
        User,
        Users,
        Supplier,
        Category,
        Product,
        Warehouse,
        Order,
        Invoice,
        Delivery,
        Stock,
        MoneyIn,
        MoneyOut,
        Voucher,
        DetailLedger,
        GeneralReport,
        Revenue,
        Search,
        Logout,
        Exit,
        Refresh,
        Home,
        Keyboard,
        Help
    }

    public static class UiIconProvider
    {
        private static readonly Dictionary<string, Image> Cache = new Dictionary<string, Image>();

        public static Image GetIcon(UiIconType type, int size, Color color)
        {
            string key = string.Format("{0}_{1}_{2}", type, size, color.ToArgb());
            if (Cache.ContainsKey(key))
            {
                return Cache[key];
            }

            Bitmap bmp = new Bitmap(size, size);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                DrawIcon(g, type, size, color);
            }

            Cache[key] = bmp;
            return bmp;
        }

        public static Image GetIconBadge(UiIconType type, int badgeSize, Color bgColor, Color iconColor)
        {
            string key = string.Format("badge_{0}_{1}_{2}_{3}", type, badgeSize, bgColor.ToArgb(), iconColor.ToArgb());
            if (Cache.ContainsKey(key))
            {
                return Cache[key];
            }

            Bitmap bmp = new Bitmap(badgeSize, badgeSize);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (GraphicsPath path = CreateRoundedRectanglePath(new RectangleF(0.5f, 0.5f, badgeSize - 1f, badgeSize - 1f), badgeSize * 0.26f))
                using (Brush brush = new SolidBrush(bgColor))
                using (Pen pen = new Pen(Color.FromArgb(40, iconColor), 1f))
                {
                    g.FillPath(brush, path);
                    g.DrawPath(pen, path);
                }

                int iconSize = (int)(badgeSize * 0.52f);
                if (iconSize % 2 != 0) iconSize++;
                int offset = (badgeSize - iconSize) / 2;

                using (Bitmap iconBmp = (Bitmap)GetIcon(type, iconSize, iconColor))
                {
                    g.DrawImage(iconBmp, offset, offset, iconSize, iconSize);
                }
            }

            Cache[key] = bmp;
            return bmp;
        }

        public static GraphicsPath CreateRoundedRectanglePath(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            float diameter = radius * 2f;
            if (diameter > rect.Width) diameter = rect.Width;
            if (diameter > rect.Height) diameter = rect.Height;

            RectangleF arc = new RectangleF(rect.X, rect.Y, diameter, diameter);
            path.AddArc(arc, 180, 90);
            arc.X = rect.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = rect.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = rect.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();
            return path;
        }

        private static void DrawRoundedRect(Graphics g, Brush b, float x, float y, float w, float h, float r)
        {
            using (GraphicsPath p = CreateRoundedRectanglePath(new RectangleF(x, y, w, h), r))
            {
                g.FillPath(b, p);
            }
        }

        private static void DrawIcon(Graphics g, UiIconType type, int size, Color color)
        {
            float s = size;
            switch (type)
            {
                case UiIconType.Dashboard:
                    {
                        float pad = s * 0.12f;
                        float gap = s * 0.14f;
                        float tileSize = (s - 2 * pad - gap) / 2f;
                        float r = tileSize * 0.28f;
                        using (Brush b = new SolidBrush(color))
                        {
                            DrawRoundedRect(g, b, pad, pad, tileSize, tileSize, r);
                            DrawRoundedRect(g, b, pad + tileSize + gap, pad, tileSize, tileSize, r);
                            DrawRoundedRect(g, b, pad, pad + tileSize + gap, tileSize, tileSize, r);
                            DrawRoundedRect(g, b, pad + tileSize + gap, pad + tileSize + gap, tileSize, tileSize, r);
                        }
                    }
                    break;

                case UiIconType.Home:
                    using (GraphicsPath p = new GraphicsPath())
                    using (Brush b = new SolidBrush(color))
                    {
                        p.AddPolygon(new PointF[] {
                            new PointF(s * 0.50f, s * 0.12f),
                            new PointF(s * 0.88f, s * 0.48f),
                            new PointF(s * 0.76f, s * 0.48f),
                            new PointF(s * 0.76f, s * 0.88f),
                            new PointF(s * 0.24f, s * 0.88f),
                            new PointF(s * 0.24f, s * 0.48f),
                            new PointF(s * 0.12f, s * 0.48f)
                        });
                        g.FillPath(b, p);
                    }
                    break;

                case UiIconType.System:
                    {
                        float cx = s * 0.5f, cy = s * 0.5f;
                        float rOuter = s * 0.42f, rInner = s * 0.28f, rHole = s * 0.16f;
                        using (GraphicsPath p = new GraphicsPath())
                        using (Brush b = new SolidBrush(color))
                        {
                            int teeth = 6;
                            List<PointF> pts = new List<PointF>();
                            for (int i = 0; i < teeth * 2; i++)
                            {
                                float angle = (float)(i * Math.PI / teeth);
                                float r = (i % 2 == 0) ? rOuter : rInner;
                                pts.Add(new PointF(cx + r * (float)Math.Cos(angle), cy + r * (float)Math.Sin(angle)));
                            }
                            p.AddPolygon(pts.ToArray());
                            p.AddEllipse(cx - rHole, cy - rHole, rHole * 2, rHole * 2);
                            p.FillMode = FillMode.Alternate;
                            g.FillPath(b, p);
                        }
                    }
                    break;

                case UiIconType.Key:
                    using (Pen pen = new Pen(color, Math.Max(1.4f, s * 0.11f)))
                    using (Brush b = new SolidBrush(color))
                    {
                        pen.StartCap = LineCap.Round;
                        pen.EndCap = LineCap.Round;
                        float headR = s * 0.18f;
                        float headCx = s * 0.32f, headCy = s * 0.38f;
                        g.DrawEllipse(pen, headCx - headR, headCy - headR, headR * 2, headR * 2);
                        g.DrawLine(pen, headCx + headR * 0.707f, headCy + headR * 0.707f, s * 0.82f, s * 0.82f);
                        g.DrawLine(pen, s * 0.68f, s * 0.68f, s * 0.78f, s * 0.58f);
                        g.DrawLine(pen, s * 0.82f, s * 0.82f, s * 0.90f, s * 0.74f);
                    }
                    break;

                case UiIconType.Shield:
                    using (GraphicsPath p = new GraphicsPath())
                    using (Brush b = new SolidBrush(color))
                    {
                        p.AddBezier(s * 0.16f, s * 0.18f, s * 0.50f, s * 0.12f, s * 0.50f, s * 0.12f, s * 0.84f, s * 0.18f);
                        p.AddBezier(s * 0.84f, s * 0.18f, s * 0.86f, s * 0.60f, s * 0.60f, s * 0.80f, s * 0.50f, s * 0.88f);
                        p.AddBezier(s * 0.50f, s * 0.88f, s * 0.40f, s * 0.80f, s * 0.14f, s * 0.60f, s * 0.16f, s * 0.18f);
                        p.CloseFigure();
                        g.FillPath(b, p);
                    }
                    break;

                case UiIconType.User:
                    using (Brush b = new SolidBrush(color))
                    {
                        float headR = s * 0.18f;
                        g.FillEllipse(b, s * 0.50f - headR, s * 0.14f, headR * 2, headR * 2);
                        using (GraphicsPath p = new GraphicsPath())
                        {
                            p.AddArc(s * 0.16f, s * 0.52f, s * 0.68f, s * 0.58f, 180, 180);
                            p.CloseFigure();
                            g.FillPath(b, p);
                        }
                    }
                    break;

                case UiIconType.Users:
                    using (Brush b = new SolidBrush(color))
                    using (Brush bLight = new SolidBrush(Color.FromArgb(170, color)))
                    {
                        g.FillEllipse(bLight, s * 0.52f, s * 0.16f, s * 0.26f, s * 0.26f);
                        g.FillPie(bLight, s * 0.42f, s * 0.46f, s * 0.48f, s * 0.44f, 180, 180);

                        g.FillEllipse(b, s * 0.18f, s * 0.24f, s * 0.32f, s * 0.32f);
                        g.FillPie(b, s * 0.08f, s * 0.56f, s * 0.52f, s * 0.46f, 180, 180);
                    }
                    break;

                case UiIconType.Supplier:
                    using (Brush b = new SolidBrush(color))
                    {
                        PointF[] roof = new PointF[] {
                            new PointF(s * 0.14f, s * 0.84f),
                            new PointF(s * 0.14f, s * 0.42f),
                            new PointF(s * 0.44f, s * 0.26f),
                            new PointF(s * 0.44f, s * 0.42f),
                            new PointF(s * 0.74f, s * 0.26f),
                            new PointF(s * 0.74f, s * 0.42f),
                            new PointF(s * 0.86f, s * 0.36f),
                            new PointF(s * 0.86f, s * 0.84f)
                        };
                        g.FillPolygon(b, roof);
                        using (Brush bHole = new SolidBrush(Color.White))
                        {
                            g.FillRectangle(bHole, s * 0.44f, s * 0.62f, s * 0.18f, s * 0.22f);
                        }
                    }
                    break;

                case UiIconType.Category:
                    using (Brush b = new SolidBrush(color))
                    {
                        PointF[] tab = new PointF[] {
                            new PointF(s * 0.14f, s * 0.30f),
                            new PointF(s * 0.44f, s * 0.30f),
                            new PointF(s * 0.54f, s * 0.42f),
                            new PointF(s * 0.86f, s * 0.42f),
                            new PointF(s * 0.86f, s * 0.78f),
                            new PointF(s * 0.14f, s * 0.78f)
                        };
                        g.FillPolygon(b, tab);
                    }
                    break;

                case UiIconType.Product:
                    {
                        float cx = s * 0.5f, cy = s * 0.46f;
                        float w = s * 0.36f, h = s * 0.20f, d = s * 0.36f;
                        using (Brush bTop = new SolidBrush(color))
                        using (Brush bLeft = new SolidBrush(Color.FromArgb(210, color)))
                        using (Brush bRight = new SolidBrush(Color.FromArgb(160, color)))
                        {
                            g.FillPolygon(bTop, new PointF[] {
                                new PointF(cx, cy - h),
                                new PointF(cx + w, cy),
                                new PointF(cx, cy + h),
                                new PointF(cx - w, cy)
                            });
                            g.FillPolygon(bLeft, new PointF[] {
                                new PointF(cx - w, cy),
                                new PointF(cx, cy + h),
                                new PointF(cx, cy + h + d),
                                new PointF(cx - w, cy + d)
                            });
                            g.FillPolygon(bRight, new PointF[] {
                                new PointF(cx + w, cy),
                                new PointF(cx, cy + h),
                                new PointF(cx, cy + h + d),
                                new PointF(cx + w, cy + d)
                            });
                        }
                    }
                    break;

                case UiIconType.Warehouse:
                    using (Brush b = new SolidBrush(color))
                    using (Pen pen = new Pen(color, Math.Max(1.2f, s * 0.08f)))
                    {
                        g.FillPolygon(b, new PointF[] {
                            new PointF(s * 0.50f, s * 0.14f),
                            new PointF(s * 0.88f, s * 0.38f),
                            new PointF(s * 0.12f, s * 0.38f)
                        });
                        g.DrawRectangle(pen, s * 0.16f, s * 0.38f, s * 0.68f, s * 0.48f);
                        g.FillRectangle(b, s * 0.36f, s * 0.54f, s * 0.28f, s * 0.32f);
                    }
                    break;

                case UiIconType.Order:
                    using (Pen pen = new Pen(color, Math.Max(1.3f, s * 0.09f)))
                    {
                        g.DrawRectangle(pen, s * 0.20f, s * 0.14f, s * 0.60f, s * 0.74f);
                        float lw = s * 0.38f;
                        g.DrawLine(pen, s * 0.31f, s * 0.34f, s * 0.31f + lw, s * 0.34f);
                        g.DrawLine(pen, s * 0.31f, s * 0.50f, s * 0.31f + lw, s * 0.50f);
                        g.DrawLine(pen, s * 0.31f, s * 0.66f, s * 0.31f + lw * 0.60f, s * 0.66f);
                    }
                    break;

                case UiIconType.Invoice:
                    using (Pen pen = new Pen(color, Math.Max(1.3f, s * 0.09f)))
                    {
                        PointF[] receiptPts = new PointF[] {
                            new PointF(s * 0.20f, s * 0.12f),
                            new PointF(s * 0.80f, s * 0.12f),
                            new PointF(s * 0.80f, s * 0.84f),
                            new PointF(s * 0.70f, s * 0.76f),
                            new PointF(s * 0.60f, s * 0.84f),
                            new PointF(s * 0.50f, s * 0.76f),
                            new PointF(s * 0.40f, s * 0.84f),
                            new PointF(s * 0.30f, s * 0.76f),
                            new PointF(s * 0.20f, s * 0.84f)
                        };
                        g.DrawPolygon(pen, receiptPts);
                        g.DrawLine(pen, s * 0.32f, s * 0.30f, s * 0.68f, s * 0.30f);
                        g.DrawLine(pen, s * 0.32f, s * 0.44f, s * 0.68f, s * 0.44f);
                        g.DrawLine(pen, s * 0.32f, s * 0.58f, s * 0.54f, s * 0.58f);
                    }
                    break;

                case UiIconType.Delivery:
                    using (Brush b = new SolidBrush(color))
                    {
                        g.FillRectangle(b, s * 0.12f, s * 0.24f, s * 0.46f, s * 0.44f);
                        PointF[] cab = new PointF[] {
                            new PointF(s * 0.58f, s * 0.38f),
                            new PointF(s * 0.72f, s * 0.38f),
                            new PointF(s * 0.88f, s * 0.52f),
                            new PointF(s * 0.88f, s * 0.68f),
                            new PointF(s * 0.58f, s * 0.68f)
                        };
                        g.FillPolygon(b, cab);
                        float wR = s * 0.10f;
                        g.FillEllipse(b, s * 0.24f - wR, s * 0.74f - wR, wR * 2, wR * 2);
                        g.FillEllipse(b, s * 0.74f - wR, s * 0.74f - wR, wR * 2, wR * 2);
                    }
                    break;

                case UiIconType.Stock:
                    using (Brush b = new SolidBrush(color))
                    using (Pen pen = new Pen(color, Math.Max(1.2f, s * 0.08f)))
                    {
                        float barW = s * 0.18f;
                        float baseH = s * 0.82f;
                        g.FillRectangle(b, s * 0.15f, baseH - s * 0.32f, barW, s * 0.32f);
                        g.FillRectangle(b, s * 0.41f, baseH - s * 0.52f, barW, s * 0.52f);
                        g.FillRectangle(b, s * 0.67f, baseH - s * 0.70f, barW, s * 0.70f);
                        g.DrawLine(pen, s * 0.10f, baseH, s * 0.90f, baseH);
                    }
                    break;

                case UiIconType.Revenue:
                    using (Pen pen = new Pen(color, Math.Max(1.8f, s * 0.12f)))
                    using (Brush b = new SolidBrush(color))
                    {
                        pen.StartCap = LineCap.Round;
                        pen.EndCap = LineCap.Round;
                        PointF p1 = new PointF(s * 0.14f, s * 0.76f);
                        PointF p2 = new PointF(s * 0.42f, s * 0.50f);
                        PointF p3 = new PointF(s * 0.60f, s * 0.62f);
                        PointF p4 = new PointF(s * 0.86f, s * 0.24f);
                        g.DrawLines(pen, new PointF[] { p1, p2, p3, p4 });
                        PointF[] arrow = new PointF[] {
                            p4,
                            new PointF(p4.X - s * 0.22f, p4.Y),
                            new PointF(p4.X, p4.Y + s * 0.22f)
                        };
                        g.FillPolygon(b, arrow);
                    }
                    break;

                case UiIconType.MoneyIn:
                    using (Brush b = new SolidBrush(color))
                    using (Pen pen = new Pen(color, Math.Max(1.3f, s * 0.09f)))
                    {
                        g.DrawEllipse(pen, s * 0.20f, s * 0.42f, s * 0.60f, s * 0.44f);
                        pen.Width = Math.Max(1.8f, s * 0.12f);
                        g.DrawLine(pen, s * 0.50f, s * 0.12f, s * 0.50f, s * 0.48f);
                        PointF[] arrow = new PointF[] {
                            new PointF(s * 0.50f, s * 0.58f),
                            new PointF(s * 0.35f, s * 0.40f),
                            new PointF(s * 0.65f, s * 0.40f)
                        };
                        g.FillPolygon(b, arrow);
                    }
                    break;

                case UiIconType.MoneyOut:
                    using (Brush b = new SolidBrush(color))
                    using (Pen pen = new Pen(color, Math.Max(1.3f, s * 0.09f)))
                    {
                        g.DrawEllipse(pen, s * 0.20f, s * 0.42f, s * 0.60f, s * 0.44f);
                        pen.Width = Math.Max(1.8f, s * 0.12f);
                        g.DrawLine(pen, s * 0.50f, s * 0.60f, s * 0.50f, s * 0.24f);
                        PointF[] arrow = new PointF[] {
                            new PointF(s * 0.50f, s * 0.14f),
                            new PointF(s * 0.35f, s * 0.32f),
                            new PointF(s * 0.65f, s * 0.32f)
                        };
                        g.FillPolygon(b, arrow);
                    }
                    break;

                case UiIconType.Voucher:
                case UiIconType.DetailLedger:
                    using (Pen pen = new Pen(color, Math.Max(1.3f, s * 0.09f)))
                    using (Brush b = new SolidBrush(color))
                    {
                        g.DrawRectangle(pen, s * 0.20f, s * 0.14f, s * 0.60f, s * 0.74f);
                        g.FillRectangle(b, s * 0.30f, s * 0.22f, s * 0.40f, s * 0.16f);
                        g.DrawLine(pen, s * 0.30f, s * 0.50f, s * 0.70f, s * 0.50f);
                        g.DrawLine(pen, s * 0.30f, s * 0.64f, s * 0.60f, s * 0.64f);
                    }
                    break;

                case UiIconType.GeneralReport:
                    using (Brush b = new SolidBrush(color))
                    using (Brush bLight = new SolidBrush(Color.FromArgb(170, color)))
                    {
                        g.FillPie(b, s * 0.14f, s * 0.14f, s * 0.72f, s * 0.72f, 0, 240);
                        g.FillPie(bLight, s * 0.18f, s * 0.10f, s * 0.72f, s * 0.72f, 240, 120);
                    }
                    break;

                case UiIconType.Search:
                    using (Pen pen = new Pen(color, Math.Max(1.8f, s * 0.12f)))
                    {
                        float r = s * 0.24f;
                        float cx = s * 0.40f, cy = s * 0.40f;
                        g.DrawEllipse(pen, cx - r, cy - r, r * 2, r * 2);
                        pen.Width = Math.Max(2.2f, s * 0.15f);
                        pen.StartCap = LineCap.Round;
                        pen.EndCap = LineCap.Round;
                        g.DrawLine(pen, cx + r * 0.707f, cy + r * 0.707f, s * 0.85f, s * 0.85f);
                    }
                    break;

                case UiIconType.Refresh:
                    using (Pen pen = new Pen(color, Math.Max(1.8f, s * 0.12f)))
                    using (Brush b = new SolidBrush(color))
                    {
                        g.DrawArc(pen, s * 0.18f, s * 0.18f, s * 0.64f, s * 0.64f, 45, 270);
                        PointF tip = new PointF(s * 0.76f, s * 0.30f);
                        g.FillPolygon(b, new PointF[] {
                            tip,
                            new PointF(tip.X - s * 0.18f, tip.Y - s * 0.10f),
                            new PointF(tip.X - s * 0.10f, tip.Y + s * 0.18f)
                        });
                    }
                    break;

                case UiIconType.Logout:
                    using (Pen pen = new Pen(color, Math.Max(1.6f, s * 0.10f)))
                    using (Brush b = new SolidBrush(color))
                    {
                        g.DrawLines(pen, new PointF[] {
                            new PointF(s * 0.50f, s * 0.16f),
                            new PointF(s * 0.18f, s * 0.16f),
                            new PointF(s * 0.18f, s * 0.84f),
                            new PointF(s * 0.50f, s * 0.84f)
                        });
                        pen.Width = Math.Max(1.8f, s * 0.12f);
                        g.DrawLine(pen, s * 0.34f, s * 0.50f, s * 0.82f, s * 0.50f);
                        PointF[] arrow = new PointF[] {
                            new PointF(s * 0.86f, s * 0.50f),
                            new PointF(s * 0.68f, s * 0.36f),
                            new PointF(s * 0.68f, s * 0.64f)
                        };
                        g.FillPolygon(b, arrow);
                    }
                    break;

                case UiIconType.Exit:
                    using (Pen pen = new Pen(color, Math.Max(1.8f, s * 0.12f)))
                    {
                        pen.StartCap = LineCap.Round;
                        pen.EndCap = LineCap.Round;
                        g.DrawEllipse(pen, s * 0.14f, s * 0.14f, s * 0.72f, s * 0.72f);
                        g.DrawLine(pen, s * 0.34f, s * 0.34f, s * 0.66f, s * 0.66f);
                        g.DrawLine(pen, s * 0.66f, s * 0.34f, s * 0.34f, s * 0.66f);
                    }
                    break;

                case UiIconType.Keyboard:
                    using (Pen pen = new Pen(color, Math.Max(1.4f, s * 0.08f)))
                    using (Brush b = new SolidBrush(color))
                    {
                        float kw = s * 0.84f;
                        float kh = s * 0.54f;
                        float kx = s * 0.08f;
                        float ky = s * 0.23f;
                        using (GraphicsPath path = CreateRoundedRectanglePath(new RectangleF(kx, ky, kw, kh), s * 0.10f))
                        {
                            g.DrawPath(pen, path);
                        }
                        float dotW = s * 0.10f;
                        float dotH = s * 0.08f;
                        // Phím hàng 1
                        g.FillRectangle(b, kx + s * 0.10f, ky + s * 0.12f, dotW, dotH);
                        g.FillRectangle(b, kx + s * 0.26f, ky + s * 0.12f, dotW, dotH);
                        g.FillRectangle(b, kx + s * 0.42f, ky + s * 0.12f, dotW, dotH);
                        g.FillRectangle(b, kx + s * 0.58f, ky + s * 0.12f, dotW, dotH);
                        // Phím cách Spacebar
                        g.FillRectangle(b, kx + s * 0.18f, ky + s * 0.28f, s * 0.44f, dotH);
                    }
                    break;

                case UiIconType.Help:
                    using (Pen pen = new Pen(color, Math.Max(1.8f, s * 0.11f)))
                    using (Brush b = new SolidBrush(color))
                    {
                        pen.StartCap = LineCap.Round;
                        pen.EndCap = LineCap.Round;
                        g.DrawEllipse(pen, s * 0.12f, s * 0.12f, s * 0.76f, s * 0.76f);
                        g.DrawArc(pen, s * 0.35f, s * 0.26f, s * 0.30f, s * 0.24f, 180, 180);
                        g.DrawLine(pen, s * 0.65f, s * 0.38f, s * 0.50f, s * 0.48f);
                        g.DrawLine(pen, s * 0.50f, s * 0.48f, s * 0.50f, s * 0.58f);
                        g.FillEllipse(b, s * 0.44f, s * 0.66f, s * 0.12f, s * 0.12f);
                    }
                    break;

                default:
                    using (Brush b = new SolidBrush(color))
                    {
                        g.FillEllipse(b, s * 0.25f, s * 0.25f, s * 0.5f, s * 0.5f);
                    }
                    break;
            }
        }
    }

    public class ModernMenuRenderer : ToolStripProfessionalRenderer
    {
        public ModernMenuRenderer() : base(new ModernColorTable())
        {
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Selected || e.Item.Pressed)
            {
                Rectangle rc = new Rectangle(2, 1, e.Item.Width - 4, e.Item.Height - 2);
                using (GraphicsPath path = UiIconProvider.CreateRoundedRectanglePath(rc, 4f))
                using (Brush b = new SolidBrush(Color.FromArgb(237, 233, 254)))
                using (Pen p = new Pen(Color.FromArgb(196, 181, 253)))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.FillPath(b, path);
                    e.Graphics.DrawPath(p, path);
                }
            }
            else if (e.Item.Owner is MenuStrip)
            {
                base.OnRenderMenuItemBackground(e);
            }
            else
            {
                using (Brush b = new SolidBrush(Color.White))
                {
                    e.Graphics.FillRectangle(b, new Rectangle(Point.Empty, e.Item.Size));
                }
            }
        }
    }

    public class ModernColorTable : ProfessionalColorTable
    {
        public override Color ToolStripDropDownBackground { get { return Color.White; } }
        public override Color ImageMarginGradientBegin { get { return Color.White; } }
        public override Color ImageMarginGradientMiddle { get { return Color.White; } }
        public override Color ImageMarginGradientEnd { get { return Color.White; } }
        public override Color MenuBorder { get { return UiTheme.Border; } }
        public override Color MenuItemBorder { get { return Color.FromArgb(196, 181, 253); } }
        public override Color MenuItemSelected { get { return Color.FromArgb(237, 233, 254); } }
        public override Color MenuItemSelectedGradientBegin { get { return Color.FromArgb(237, 233, 254); } }
        public override Color MenuItemSelectedGradientEnd { get { return Color.FromArgb(237, 233, 254); } }
        public override Color MenuItemPressedGradientBegin { get { return Color.FromArgb(221, 214, 254); } }
        public override Color MenuItemPressedGradientEnd { get { return Color.FromArgb(221, 214, 254); } }
        public override Color SeparatorDark { get { return UiTheme.Border; } }
        public override Color SeparatorLight { get { return Color.White; } }
    }
}
